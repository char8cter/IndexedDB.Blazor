using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorIndexedDbSample.Interfaces
{
    public interface IReadOnlyObjectStore<TKey, TValue> : IReadOnlyObjectIndex<TKey, TValue>
    {
        #region Properties
        IReadOnlyList<string> IndexNames { get; }
        #endregion

        #region Operations
        Task<IReadOnlyObjectIndex<TKey, TValue>> RetrieveIndexAsync(string name);
        #endregion
    }
}
