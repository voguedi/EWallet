using System.Threading.Tasks;
using EWallet.Accounting.Commands;
using EWallet.Accounting.Domain;
using EWallet.Accounting.Domain.AccountAggregate;
using EWallet.Accounting.Domain.DepositTransactionAggregate;
using Voguedi.AsyncExecution;
using Voguedi.Commands;
using Voguedi.Domain.Events;

namespace EWallet.Accounting.ProcessManagers
{
    class DepositTransactionProcessManager
        : IEventHandler<DepositTransactionCommittedEvent>
        , IEventHandler<TransactionPreparationAddedEvent>
        , IEventHandler<DepositTransactionPreparationConfirmedEvent>
        , IEventHandler<TransactionPreparationCommittedEvent>
    {
        #region Private Fields

        readonly ICommandSender commandSender;

        #endregion

        #region Ctors

        public DepositTransactionProcessManager(ICommandSender commandSender) => this.commandSender = commandSender;

        #endregion

        #region IEventHandler<DepositTransactionCommittedEvent>

        public Task<AsyncExecutedResult> HandleAsync(DepositTransactionCommittedEvent e)
            => commandSender.SendAsync(new AddTransactionPreparationCommand(e.AccountId, e.AggregateRootId, TransactionType.Deposit, PreparationType.Income, e.Amount) { Id = e.Id });

        #endregion

        #region IEventHandler<TransactionPreparationAddedEvent>

        public Task<AsyncExecutedResult> HandleAsync(TransactionPreparationAddedEvent e)
        {
            var transactionPreparation = e.TransactionPreparation;

            if (transactionPreparation.TransactionType == TransactionType.Deposit && transactionPreparation.PreparationType == PreparationType.Income)
                commandSender.SendAsync(new ConfirmDepositTransactionPreparationCommand(e.TransactionPreparation.Id) { Id = e.Id });

            return Task.FromResult(AsyncExecutedResult.Success);
        }

        #endregion

        #region IEventHandler<DepositTransactionPreparationConfirmedEvent>

        public Task<AsyncExecutedResult> HandleAsync(DepositTransactionPreparationConfirmedEvent e)
            => commandSender.SendAsync(new CommitTransactionPreparationCommand(e.AccountId, e.AggregateRootId) { Id = e.Id });

        #endregion

        #region IEventHandler<TransactionPreparationCommittedEvent>

        public Task<AsyncExecutedResult> HandleAsync(TransactionPreparationCommittedEvent e)
        {
            var transactionPreparation = e.TransactionPreparation;

            if (transactionPreparation.TransactionType == TransactionType.Deposit && transactionPreparation.PreparationType == PreparationType.Income)
                commandSender.SendAsync(new ConfirmDepositTransactionCommand(transactionPreparation.Id) { Id = e.Id });

            return Task.FromResult(AsyncExecutedResult.Success);
        }

        #endregion
    }
}
