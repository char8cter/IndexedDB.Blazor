using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorIndexedDbSample.Interfaces
{
    public interface IObjectStore<TKey, TValue> : IObjectIndex<TKey, TValue>
    {
        #region Properties
        IReadOnlyList<string> IndexNames { get; }
        #endregion

        #region Operations
        Task<IObjectIndex<TKey, TValue>> RetrieveIndexAsync(string indexName);
        #endregion
    }
}
