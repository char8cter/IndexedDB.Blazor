using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.JSInterop;

namespace BlazorIndexedDbSample.Database
{
    public class ObjectStoreOperationHandler<T>
    {
        #region Accessors
        public TaskCompletionSource<T> TaskCompletionSource { get; }
        #endregion

        #region Constructors
        public ObjectStoreOperationHandler()
        {
            this.TaskCompletionSource = new TaskCompletionSource<T>();
        }
        #endregion

        #region Handlers
        [JSInvokable]
        public void OnSucceeded(T result)
        {
            this.TaskCompletionSource.SetResult(result);
        }

        [JSInvokable]
        public void OnFailed(T result)
        {
            this.TaskCompletionSource.SetResult(result);
        }
        #endregion
    }
}
