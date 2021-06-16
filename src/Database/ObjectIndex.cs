using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.JSInterop;

using BlazorIndexedDbSample.Interfaces;

namespace BlazorIndexedDbSample.Database
{
    public class ObjectIndex<TKey, TValue> : IObjectIndex<TKey, TValue>
    {
        #region Readonly Properties
        private readonly IJSObjectReference _JSObjectIndexReference;
        #endregion

        #region Accessors
        public string Name { get; }
        #endregion

        #region Retrieval Operations
        public Task<IAsyncEnumerable<TKey>> RetrieveKeys()
        {
        }

        public Task<IAsyncEnumerable<TValue>> RetrieveValues()
        {
        }
        #endregion

        #region Operations
        public Task<bool> InsertAsync(TKey key, TValue value)
        {
        }
        public Task<bool> StoreAsync(TKey key, TValue value)
        {
        }
        public Task<TValue> RetrieveAsync(TKey key)
        {
        }
        public Task<bool> RemoveAsync(TKey key)
        {
        }
        #endregion
    }
}
