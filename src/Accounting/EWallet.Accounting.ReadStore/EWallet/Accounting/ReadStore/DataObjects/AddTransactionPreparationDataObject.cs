using System;

namespace EWallet.Accounting.ReadStore.DataObjects
{
    public class AddTransactionPreparationDataObject
    {
        #region Public Properties

        public string AccountId { get; set; }

        public long Version { get; set; }

        public string Id { get; set; }

        public TransactionTypeDataObject TransactionType { get; set; }

        public PreparationTypeDataObject PreparationType { get; set; }

        public double Amount { get; set; }

        public DateTime AddedOn { get; set; }

        #endregion
    }
}
