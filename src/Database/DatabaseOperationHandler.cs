using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.JSInterop;

namespace BlazorIndexedDbSample.Database
{
    public class DatabaseOperationHandler
    {
        #region Properties
        private readonly DatabaseConnection.VersionChangedDelegate VersionChanged;
        #endregion

        #region Accessors
        public TaskCompletionSource<bool> TaskCompletionSource { get; }        
        #endregion

        #region Constructors
        public DatabaseOperationHandler(DatabaseConnection.VersionChangedDelegate versionChangedCallback)
        {
            this.VersionChanged = versionChangedCallback;
            this.TaskCompletionSource = new TaskCompletionSource<bool>();
        }
        #endregion

        #region Handlers
        [JSInvokable]
        public void OnOpenSucceeded()
        {
            this.TaskCompletionSource.SetResult(true);
        }

        [JSInvokable]
        public void OnOpenFailed()
        {
            this.TaskCompletionSource.SetResult(false);
        }

        [JSInvokable]
        public async void OnVersionChanged(ulong newVersion, ulong oldVersion)
        {
            bool retval;

            if (this.VersionChanged != null)
            {
                retval = await this.VersionChanged.Invoke(newVersion, oldVersion);
            }
            else
            {
                retval = true;
            }

            this.TaskCompletionSource.SetResult(retval);
        }
        #endregion
    }
}
