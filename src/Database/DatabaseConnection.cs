using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.JSInterop;

using BlazorIndexedDbSample.Interfaces;

namespace BlazorIndexedDbSample.Database
{
    public class DatabaseConnection : IDatabase
    {
        #region Delegates
        public delegate Task<bool> VersionChangedDelegate(ulong newVersion, ulong oldVersion);
        #endregion

        #region Readonly Properties
        private readonly IJSObjectReference _JSModuleReference;
        #endregion

        #region Properties
        private IJSObjectReference _JSDatabaseReference;
        #endregion

        #region Accessors
        public string Name { get; }
        public ulong Version { get; }
        public VersionChangedDelegate DatabaseVersionChanged { get; }
        public bool IsOpen { get; protected set; }
        #endregion

        #region Events
        #endregion

        #region Constructor
        public DatabaseConnection(IJSObjectReference jsModuleReference, string name, ulong version, VersionChangedDelegate databaseVersionChanged)
        {
            this._JSModuleReference = jsModuleReference;
            this._JSDatabaseReference = null;

            this.Name = name;
            this.Version = version;
            this.DatabaseVersionChanged = databaseVersionChanged;
        }
        #endregion

        #region Initialization
        public async Task InitializeAsync()
        {
            this._JSDatabaseReference = await this._JSModuleReference.InvokeAsync<IJSObjectReference>("CreateDatabaseInterop");

            //await this._JSDatabaseReference.InvokeAsync<IJSObjectReference>("Test");
        }
        #endregion

        #region Operations
        public async Task<bool> OpenAsync()
        {
            DatabaseOperationHandler openDbHandler = new DatabaseOperationHandler(this.DatabaseVersionChanged);

            DotNetObjectReference<DatabaseOperationHandler> openDbHandlerReference = DotNetObjectReference.Create(openDbHandler);
            
            this._JSDatabaseReference.InvokeVoidAsync("Open", this.Name, this.Version, openDbHandlerReference);
            
            this.IsOpen = await openDbHandler.TaskCompletionSource.Task;

            return this.IsOpen;
        }
        public async Task CloseAsync()
        {
            await this._JSDatabaseReference.InvokeVoidAsync("Close").AsTask();

            this.IsOpen = false;
        }
        #endregion

        #region Create Operations
        public async Task<ObjectStore> CreateObjectStoreAsync(string name)
        {
            ObjectStore retval;

            if (this.IsOpen && (this._JSDatabaseReference != null))
            {
                IJSObjectReference objectStoreReference = await this._JSDatabaseReference.InvokeAsync<IJSObjectReference>("CreateObjectStore", name);

                retval = new ObjectStore(objectStoreReference);
            }
            else
            {
                throw new Exception("Invalid State: Database must be open to create a new Object Store.");
            }

            return retval;
        }
        public async Task<IDatabaseTransaction> CreateTransactionAsync(string objectStoreName)
        {
            Transaction retval;

            if (this.IsOpen && (this._JSDatabaseReference != null))
            {
                IJSObjectReference transactionReference = await this._JSDatabaseReference.InvokeAsync<IJSObjectReference>("CreateTransaction", objectStoreName);

                retval = new Transaction(transactionReference);
            }
            else
            {
                throw new Exception("Invalid State: Database must be open to create a new Object Store.");
            }

            return retval;
        }
        #endregion

        #region
        public async Task<IReadOnlyList<string>> RetrieveObjectStoreNames()
        {
            IReadOnlyList<string> retval;

            if (this.IsOpen && (this._JSDatabaseReference != null))
            {
                string[] objectStoreNames = await this._JSDatabaseReference.InvokeAsync<string[]>("RetrieveObjectStoreNames");

                retval = objectStoreNames;
            }
            else
            {
                throw new Exception("Invalid State: Database must be open to retrieve Object Store Names.");
            }

            return retval;
        }
        #endregion
    }
}