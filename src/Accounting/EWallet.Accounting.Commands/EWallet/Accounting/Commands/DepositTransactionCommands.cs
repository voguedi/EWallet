using System;
using System.Threading.Tasks;
using EWallet.Accounting.Domain.DepositTransactionAggregate;
using Voguedi.Commands;

namespace EWallet.Accounting.Commands
{
    #region CommitDepositTransactionCommand

    [CommandSubscriber("deposit", GroupName = "transaction.commands")]
    public class CommitDepositTransactionCommand : Command<string>
    {
        #region Public Properties

        public string AccountId { get; }

        public double Amount { get; }

        #endregion

        #region Ctors

        public CommitDepositTransactionCommand() { }

        public CommitDepositTransactionCommand(string transactionId, string accountId, double amount)
            : base(transactionId)
        {
            if (string.IsNullOrWhiteSpace(accountId))
                throw new ArgumentNullException(nameof(accountId));

            if (amount <= 0D)
                throw new ArgumentOutOfRangeException(nameof(amount));

            AccountId = accountId;
            Amount = amount;
        }

        #endregion
    }

    #endregion

    #region ConfirmDepositTransactionPreparationCommand

    [CommandSubscriber("deposit", GroupName = "transaction.commands")]
    public class ConfirmDepositTransactionPreparationCommand : Command<string>
    {
        #region Ctors

        public ConfirmDepositTransactionPreparationCommand() { }

        public ConfirmDepositTransactionPreparationCommand(string transactionId) : base(transactionId) { }

        #endregion
    }

    #endregion

    #region ConfirmDepositTransactionCommand

    [CommandSubscriber("deposit", GroupName = "transaction.commands")]
    public class ConfirmDepositTransactionCommand : Command<string>
    {
        #region Ctors

        public ConfirmDepositTransactionCommand() { }

        public ConfirmDepositTransactionCommand(string transactionId) : base(transactionId) { }

        #endregion
    }

    #endregion

    #region DepositTransactionCommandHandler

    class DepositTransactionCommandHandler
        : ICommandHandler<CommitDepositTransactionCommand>
        , ICommandHandler<ConfirmDepositTransactionPreparationCommand>
        , ICommandHandler<ConfirmDepositTransactionCommand>
    {
        #region ICommandHandler<CommitDepositTransactionCommand>

        public Task HandleAsync(ICommandHandlerContext context, CommitDepositTransactionCommand command)
            => context.CreateAggregateRootAsync<DepositTransaction, string>(new DepositTransaction(command.AggregateRootId, command.AccountId, command.Amount));

        #endregion

        #region ICommandHandler<ConfirmDepositTransactionPreparationCommand>

        public async Task HandleAsync(ICommandHandlerContext context, ConfirmDepositTransactionPreparationCommand command)
        {
            var transaction = await context.GetAggregateRootAsync<DepositTransaction, string>(command.AggregateRootId);
            transaction.ConfirmPreparation();
        }

        #endregion

        #region ICommandHandler<ConfirmDepositTransactionCommand>

        public async Task HandleAsync(ICommandHandlerContext context, ConfirmDepositTransactionCommand command)
        {
            var transaction = await context.GetAggregateRootAsync<DepositTransaction, string>(command.AggregateRootId);
            transaction.Confirm();
        }

        #endregion
    }

    #endregion
}
