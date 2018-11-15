using Voguedi.Domain.AggregateRoots;

namespace EWallet.Accounting.Domain.DepositTransactionAggregate
{
    public class DepositTransaction : AggregateRoot<string>
    {
        #region Public Properties

        public string AccountId { get; private set; }

        public double Amount { get; private set; }

        public TransactionStatus Status { get; private set; }

        #endregion

        #region Ctors

        public DepositTransaction() { }

        public DepositTransaction(string id) : base(id) { }

        public DepositTransaction(string id, string accountId, double amount) : this(id) => ApplyEvent(new DepositTransactionCommittedEvent(accountId, amount));

        #endregion

        #region Private Methods

        void Handle(DepositTransactionCommittedEvent e)
        {
            AccountId = e.AccountId;
            Amount = e.Amount;
            Status = TransactionStatus.Committed;
        }

        void Handle(DepositTransactionPreparationConfirmedEvent e) => Status = TransactionStatus.PreparationConfirmed;

        void Handle(DepositTransactionConfirmedEvent e) => Status = TransactionStatus.Confirmed;

        #endregion

        #region Public Methods

        public void ConfirmPreparation()
        {
            if (Status == TransactionStatus.Committed)
                ApplyEvent(new DepositTransactionPreparationConfirmedEvent(AccountId));
        }

        public void Confirm()
        {
            if (Status == TransactionStatus.PreparationConfirmed)
                ApplyEvent(new DepositTransactionConfirmedEvent(AccountId));
        }

        #endregion
    }
}
