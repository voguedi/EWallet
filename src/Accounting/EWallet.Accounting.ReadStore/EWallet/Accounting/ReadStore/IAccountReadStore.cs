using System.Threading.Tasks;
using EWallet.Accounting.ReadStore.DataObjects;
using Voguedi.AsyncExecution;

namespace EWallet.Accounting.ReadStore
{
    public interface IAccountReadStore
    {
        #region Methods

        Task<AsyncExecutedResult> CreateAsync(CreateAccountDataObject dataObject);

        Task<AsyncExecutedResult> AddTransactionPreparationAsync(AddTransactionPreparationDataObject dataObject);

        Task<AsyncExecutedResult> CommitTransactionPreparationAsync(CommitTransactionPreparationDataObject dataObject);

        #endregion
    }
}
