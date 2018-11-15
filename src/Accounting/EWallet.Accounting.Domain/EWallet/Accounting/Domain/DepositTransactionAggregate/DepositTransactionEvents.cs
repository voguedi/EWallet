using System;
using System.Threading.Tasks;
using Voguedi.AsyncExecution;
using Voguedi.Domain.Events;

namespace EWallet.Accounting.Domain.DepositTransactionAggregate
{
    #region DepositTransactionCommittedEvent

    [EventSubscriber("deposit", GroupName = "transaction.events")]
    public class DepositTransactionCommittedEvent : Event<string>
    {
        #region Public Properties

        public string AccountId { get; }

        public double Amount { get; }

        #endregion

        #region Ctors

        public DepositTransactionCommittedEvent() { }

        public DepositTransactionCommittedEvent(string accountId, double amount)
        {
            AccountId = accountId;
            Amount = amount;
        }

        #endregion
    }

    #endregion

    #region DepositTransactionPreparationConfirmedEvent

    [EventSubscriber("deposit", GroupName = "transaction.events")]
    public class DepositTransactionPreparationConfirmedEvent : Event<string>
    {
        #region Public Properties

        public string AccountId { get; }

        #endregion

        #region Ctors

        public DepositTransactionPreparationConfirmedEvent() { }

        public DepositTransactionPreparationConfirmedEvent(string accountId) => AccountId = accountId;

        #endregion
    }

    #endregion

    #region DepositTransactionConfirmedEvent

    [EventSubscriber("deposit", GroupName = "transaction.events")]
    public class DepositTransactionConfirmedEvent : Event<string>
    {
        #region Public Properties

        public string AccountId { get; }

        #endregion

        #region Ctors

        public DepositTransactionConfirmedEvent() { }

        public DepositTransactionConfirmedEvent(string accountId) => AccountId = accountId;

        #endregion
    }

    #endregion

    #region DepositTransactionEventHandler

    class DepositTransactionEventHandler
        : IEventHandler<DepositTransactionCommittedEvent>
        , IEventHandler<DepositTransactionConfirmedEvent>
    {
        #region IEventHandler<DepositTransactionCommittedEvent>

        public Task<AsyncExecutedResult> HandleAsync(DepositTransactionCommittedEvent e)
        {
            Console.WriteLine($"存款交易已提交！ [Id = {e.Id}, Version = {e.Version}, AccountId = {e.AccountId}, Amount = {e.Amount}]");
            return Task.FromResult(AsyncExecutedResult.Success);
        }

        #endregion

        #region IEventHandler<DepositTransactionConfirmedEvent>

        public Task<AsyncExecutedResult> HandleAsync(DepositTransactionConfirmedEvent e)
        {
            Console.WriteLine($"存款交易已确认！ [Id = {e.Id}, Version = {e.Version}, AccountId = {e.AccountId}]");
            return Task.FromResult(AsyncExecutedResult.Success);
        }

        #endregion
    }

    #endregion
}
