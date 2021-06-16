using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorIndexedDbSample.Interfaces
{
    public interface IDatabaseTransaction
    {
        #region Retrieval Operations
        Task<IObjectStore<TKey, TValue>> RetrieveObjectStore<TKey, TValue>(string objectStoreName);
        #endregion

        #region Operations
        Task<bool> Commit();
        Task<bool> Rollback();
        #endregion
    }
}
