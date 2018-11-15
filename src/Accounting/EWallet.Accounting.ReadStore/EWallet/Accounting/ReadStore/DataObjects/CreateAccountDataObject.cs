using System;

namespace EWallet.Accounting.ReadStore.DataObjects
{
    public class CreateAccountDataObject
    {
        #region Public Properties

        public string Id { get; set; }

        public string Owner { get; set; }

        public long Version { get; set; }

        public DateTime CreatedOn { get; set; }

        #endregion

        #region Ctors

        public CreateAccountDataObject() { }

        public CreateAccountDataObject(string id, string owner, long version, DateTime createdOn)
        {
            Id = id;
            Owner = owner;
            Version = version;
            CreatedOn = createdOn;
        }

        #endregion
    }
}
