using System;
using System.Threading.Tasks;
using Voguedi.AsyncExecution;
using Voguedi.Domain.Events;

namespace EWallet.Accounting.Domain.AccountAggregate
{
    #region AccountCreatedEvent

    [EventSubscriber("account", GroupName = "account.events")]
    public class AccountCreatedEvent : Event<string>
    {
        #region Public Properties

        public string Owner { get; }

        #endregion

        #region Ctors

        public AccountCreatedEvent() { }

        public AccountCreatedEvent(string owner) => Owner = owner;

        #endregion
    }

    #endregion

    #region TransactionPreparationAddedEvent

    [EventSubscriber("account", GroupName = "account.events")]
    public class TransactionPreparationAddedEvent : Event<string>
    {
        #region Public Properties

        public TransactionPreparation TransactionPreparation { get; }

        #endregion

        #region Ctors

        public TransactionPreparationAddedEvent() { }

        public TransactionPreparationAddedEvent(TransactionPreparation transactionPreparation) => TransactionPreparation = transactionPreparation;

        #endregion
    }

    #endregion

    #region TransactionPreparationCommittedEvent

    [EventSubscriber("account", GroupName = "account.events")]
    public class TransactionPreparationCommittedEvent : Event<string>
    {
        #region Public Properties

        public double Balance { get; }

        public TransactionPreparation TransactionPreparation { get; }

        #endregion

        #region Ctors

        public TransactionPreparationCommittedEvent() { }

        public TransactionPreparationCommittedEvent(double balance, TransactionPreparation transactionPreparation)
        {
            Balance = balance;
            TransactionPreparation = transactionPreparation;
        }

        #endregion
    }

    #endregion

    #region AccountEventHandler

    class AccountEventHandler
        : IEventHandler<AccountCreatedEvent>
        , IEventHandler<TransactionPreparationAddedEvent>
        , IEventHandler<TransactionPreparationCommittedEvent>
    {
        #region IEventHandler<AccountCreatedEvent>

        public Task<AsyncExecutedResult> HandleAsync(AccountCreatedEvent e)
        {
            Console.WriteLine($"账户创建成功！ [Id = {e.AggregateRootId}, Version = {e.Version}, Owner = {e.Owner}]");
            return Task.FromResult(AsyncExecutedResult.Success);
        }

        #endregion

        #region IEventHandler<TransactionPreparationAddedEvent>

        public Task<AsyncExecutedResult> HandleAsync(TransactionPreparationAddedEvent e)
        {
            var preparation = e.TransactionPreparation;
            Console.WriteLine($"预交易已添加！ [Id = {e.AggregateRootId}, Version = {e.Version}, TransactionId = {preparation.Id}, TransactionType = {preparation.TransactionType}, PreparationType = {preparation.PreparationType}, Amount = {preparation.Amount}]");
            return Task.FromResult(AsyncExecutedResult.Success);
        }

        #endregion

        #region IEventHandler<TransactionPreparationCommittedEvent>
        
        public Task<AsyncExecutedResult> HandleAsync(TransactionPreparationCommittedEvent e)
        {
            Console.WriteLine($"账户创建成功！ [Id = {e.AggregateRootId}, Version = {e.Version}, Balance = {e.Balance}]");
            return Task.FromResult(AsyncExecutedResult.Success);
        }

        #endregion
    }

    #endregion
}
