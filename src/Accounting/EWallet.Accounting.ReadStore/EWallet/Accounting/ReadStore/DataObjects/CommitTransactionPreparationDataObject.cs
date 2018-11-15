using System;

namespace EWallet.Accounting.ReadStore.DataObjects
{
    public class CommitTransactionPreparationDataObject
    {
        #region Public Properties

        public string AccountId { get; set; }

        public double Balance { get; set; }

        public long Version { get; set; }

        public string Id { get; set; }

        public TransactionTypeDataObject TransactionType { get; set; }

        public PreparationTypeDataObject PreparationType { get; set; }

        public double Amount { get; set; }

        public DateTime CommittedOn { get; set; }

        #endregion
    }
}
