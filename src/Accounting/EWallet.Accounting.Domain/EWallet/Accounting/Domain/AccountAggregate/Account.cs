using System.Collections.Generic;
using Voguedi.Domain.AggregateRoots;

namespace EWallet.Accounting.Domain.AccountAggregate
{
    public class Account : AggregateRoot<string>
    {
        #region Private Fields

        readonly Dictionary<string, TransactionPreparation> transactionPreparations = new Dictionary<string, TransactionPreparation>();

        #endregion

        #region Public Properties

        public string Owner { get; private set; }

        public double Balance { get; private set; }

        public IReadOnlyDictionary<string, TransactionPreparation> TransactionPreparations => transactionPreparations;

        #endregion

        #region Ctors

        public Account() { }

        public Account(string id) : base(id) { }

        public Account(string id, string owner) : this(id) => ApplyEvent(new AccountCreatedEvent(owner));

        #endregion

        #region Private Methods

        void Handle(AccountCreatedEvent e) => Owner = e.Owner;

        void Handle(TransactionPreparationAddedEvent e) => transactionPreparations.Add(e.TransactionPreparation.Id, e.TransactionPreparation);

        void Handle(TransactionPreparationCommittedEvent e)
        {
            transactionPreparations.Remove(e.TransactionPreparation.Id);
            Balance = e.Balance;
        }

        #endregion

        #region Public Methods

        public void AddTransactionPreparation(string transactionId, TransactionType transactionType, PreparationType preparationType, double amount)
        {
            ApplyEvent(new TransactionPreparationAddedEvent(new TransactionPreparation(transactionId, Id, transactionType, preparationType, amount)));
        }

        public void CommitTransactionPreparation(string transactionId)
        {
            if (transactionPreparations.TryGetValue(transactionId, out var transactionPreparation))
            {
                var currentBalance = Balance;

                if (transactionPreparation.PreparationType == PreparationType.Income)
                    currentBalance += transactionPreparation.Amount;

                ApplyEvent(new TransactionPreparationCommittedEvent(currentBalance, transactionPreparation));
            }
        }

        #endregion
    }
}
