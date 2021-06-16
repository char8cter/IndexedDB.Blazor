using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorIndexedDbSample.Interfaces
{
    public interface IReadOnlyObjectIndex<TKey, TValue>
    {
        #region Properties
        string Name { get; }
        #endregion

        #region Retrieval Operations
        Task<IAsyncEnumerable<TKey>> RetrieveKeys { get; }
        Task<IAsyncEnumerable<TValue>> RetrieveValues { get; }
        #endregion

        #region Operations
        Task<TValue> RetrieveAsync(TKey key);
        #endregion
    }
}
