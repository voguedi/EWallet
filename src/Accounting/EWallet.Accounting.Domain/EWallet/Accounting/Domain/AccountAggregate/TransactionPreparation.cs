using Voguedi.Domain.Entities;

namespace EWallet.Accounting.Domain.AccountAggregate
{
    public class TransactionPreparation : Entity<string>
    {
        #region Public Properties

        public string AccountId { get; private set; }

        public TransactionType TransactionType { get; private set; }

        public PreparationType PreparationType { get; private set; }

        public double Amount { get; private set; }

        #endregion

        #region Ctors

        public TransactionPreparation() { }

        public TransactionPreparation(string id) : base(id) { }

        public TransactionPreparation(string id, string accountId, TransactionType transactionType, PreparationType preparationType, double amount)
            : this(id)
        {
            AccountId = accountId;
            PreparationType = preparationType;
            TransactionType = transactionType;
            Amount = amount;
        }

        #endregion
    }
}
