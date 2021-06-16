using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.JSInterop;

using BlazorIndexedDbSample.Interfaces;

namespace BlazorIndexedDbSample.Database
{
    public class ObjectStore<TKey, TValue> : IObjectStore<TKey, TValue>
    {
        #region Readonly Properties
        private readonly IJSObjectReference _JSObjectStoreReference;
        #endregion

        #region Properties
        public string Name { get; }
        public IReadOnlyList<string> IndexNames { get; }
        #endregion

        #region Constructors
        public ObjectStore(IJSObjectReference objectStoreReference)
        {
            this._JSObjectStoreReference = objectStoreReference;
        }
        #endregion

        #region Retrieval Operations
        public Task<IAsyncEnumerable<TKey>> RetrieveKeys()
        {
        }
        public Task<IAsyncEnumerable<TValue>> RetrieveValues()
        {
        }
        #endregion

        #region Index Operations
        public async Task<IObjectIndex<TKey, TValue>> RetrieveIndexAsync(string indexName)
        {
            ObjectIndex<TKey, TValue> retval;

            if (this._JSObjectStoreReference != null)
            {
                IJSObjectReference objectIndexReference = await this._JSObjectStoreReference.InvokeAsync<IJSObjectReference>("RetrieveIndex", indexName);

                retval = new ObjectIndex<TKey, TValue>(objectIndexReference);
            }
            else
            {
                throw new Exception("Invalid State: Object Store is NULL.");
            }

            return retval;
        }
        #endregion

        #region Operations
        public Task<bool> InsertAsync(TKey key, TValue value)
        {
            ObjectStoreOperationHandler<bool> requestHandler = new ObjectStoreOperationHandler<bool>();

            DotNetObjectReference<ObjectStoreOperationHandler<bool>> requestHandlerReference = DotNetObjectReference.Create(requestHandler);

            this._JSObjectStoreReference.InvokeAsync<bool>("Insert", key, value, requestHandlerReference);

            return requestHandler.TaskCompletionSource.Task;
        }
        public Task<bool> StoreAsync(TKey key, TValue value)
        {
            ObjectStoreOperationHandler<bool> requestHandler = new ObjectStoreOperationHandler<bool>();

            DotNetObjectReference<ObjectStoreOperationHandler<bool>> requestHandlerReference = DotNetObjectReference.Create(requestHandler);

            this._JSObjectStoreReference.InvokeAsync<bool>("Store", key, value, requestHandlerReference);

            return requestHandler.TaskCompletionSource.Task;
        }
        public Task<TValue> RetrieveAsync(TKey key)
        {
            ObjectStoreOperationHandler<TValue> requestHandler = new ObjectStoreOperationHandler<TValue>();

            DotNetObjectReference<ObjectStoreOperationHandler<TValue>> requestHandlerReference = DotNetObjectReference.Create(requestHandler);

            this._JSObjectStoreReference.InvokeAsync<bool>("Retrieve", key, requestHandlerReference);

            return requestHandler.TaskCompletionSource.Task;
        }
        public Task<bool> RemoveAsync(TKey key)
        {
            ObjectStoreOperationHandler<bool> requestHandler = new ObjectStoreOperationHandler<bool>();

            DotNetObjectReference<ObjectStoreOperationHandler<bool>> requestHandlerReference = DotNetObjectReference.Create(requestHandler);

            this._JSObjectStoreReference.InvokeAsync<bool>("Delete", key, requestHandlerReference);

            return requestHandler.TaskCompletionSource.Task;
        }
        #endregion

        #region Query Operations
        #endregion
    }
}
