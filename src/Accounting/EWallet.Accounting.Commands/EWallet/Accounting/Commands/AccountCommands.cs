using System;
using System.Threading.Tasks;
using EWallet.Accounting.Domain;
using EWallet.Accounting.Domain.AccountAggregate;
using Voguedi.Commands;

namespace EWallet.Accounting.Commands
{
    #region CreateAccountCommand

    [CommandSubscriber("account", GroupName = "account.commands")]
    public class CreateAccountCommand : Command<string>
    {
        #region Public Properties

        public string Owner { get; }

        #endregion

        #region Ctors

        public CreateAccountCommand() { }

        public CreateAccountCommand(string accountId, string owner)
            : base(accountId)
        {
            if (string.IsNullOrWhiteSpace(owner))
                throw new ArgumentNullException(nameof(owner));

            Owner = owner;
        }

        #endregion
    }

    #endregion

    #region AddTransactionPreparationCommand

    [CommandSubscriber("account", GroupName = "account.commands")]
    public class AddTransactionPreparationCommand : Command<string>
    {
        #region Public Properties

        public string TransactionId { get; }

        public TransactionType TransactionType { get; }

        public PreparationType PreparationType { get; }

        public double Amount { get; }

        #endregion

        #region Ctors

        public AddTransactionPreparationCommand() { }

        public AddTransactionPreparationCommand(string accountId, string transactionId, TransactionType transactionType, PreparationType preparationType, double amount)
            : base(accountId)
        {
            if (string.IsNullOrWhiteSpace(transactionId))
                throw new ArgumentNullException(nameof(transactionId));

            if (amount <= 0D)
                throw new ArgumentOutOfRangeException(nameof(amount));

            TransactionId = transactionId;
            TransactionType = transactionType;
            PreparationType = preparationType;
            Amount = amount;
        }

        #endregion
    }

    #endregion

    #region CommitTransactionPreparationCommand

    [CommandSubscriber("account", GroupName = "account.commands")]
    public class CommitTransactionPreparationCommand : Command<string>
    {
        #region Public Properties

        public string TransactionId { get; }

        #endregion

        #region Ctors

        public CommitTransactionPreparationCommand() { }

        public CommitTransactionPreparationCommand(string accountId, string transactionId)
            : base(accountId)
        {
            if (string.IsNullOrWhiteSpace(transactionId))
                throw new ArgumentNullException(nameof(transactionId));

            TransactionId = transactionId;
        }

        #endregion
    }

    #endregion

    #region AccountCommandHandler

    class AccountCommandHandler
        : ICommandHandler<CreateAccountCommand>
        , ICommandHandler<AddTransactionPreparationCommand>
        , ICommandHandler<CommitTransactionPreparationCommand>
    {
        #region ICommandHandler<CreateAccountCommand>

        public Task HandleAsync(ICommandHandlerContext context, CreateAccountCommand command)
            => context.CreateAggregateRootAsync<Account, string>(new Account(command.AggregateRootId, command.Owner));

        #endregion

        #region ICommandHandler<AddTransactionPreparationCommand>

        public async Task HandleAsync(ICommandHandlerContext context, AddTransactionPreparationCommand command)
        {
            var account = await context.GetAggregateRootAsync<Account, string>(command.AggregateRootId);
            account.AddTransactionPreparation(command.TransactionId, command.TransactionType, command.PreparationType, command.Amount);
        }

        #endregion

        #region ICommandHandler<CommitTransactionPreparationCommand>

        public async Task HandleAsync(ICommandHandlerContext context, CommitTransactionPreparationCommand command)
        {
            var account = await context.GetAggregateRootAsync<Account, string>(command.AggregateRootId);
            account.CommitTransactionPreparation(command.TransactionId);
        }

        #endregion
    }

    #endregion
}
