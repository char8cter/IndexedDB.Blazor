using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorIndexedDbSample.Interfaces
{
    public interface IObjectIndex<TKey, TValue>
    {
        #region Properties
        string Name { get; }
        #endregion

        #region Retrieval Operations
        Task<IAsyncEnumerable<TKey>> RetrieveKeys();
        Task<IAsyncEnumerable<TValue>> RetrieveValues();
        #endregion

        #region Operations
        Task<bool> InsertAsync(TKey key, TValue value);
        Task<bool> StoreAsync(TKey key, TValue value);
        Task<TValue> RetrieveAsync(TKey key);        
        Task<bool> RemoveAsync(TKey key);
        #endregion
    }
}
