using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.JSInterop;

using BlazorIndexedDbSample.Interfaces;

namespace BlazorIndexedDbSample.Database
{
    public class Transaction : IDatabaseTransaction
    {
        #region Readonly Properties
        private readonly IJSObjectReference _JSTransactionReference;
        #endregion

        #region Constructors
        public Transaction(IJSObjectReference transactionReference)
        {
            this._JSTransactionReference = transactionReference;
        }
        #endregion

        #region Retrieval Operations
        public async Task<IObjectStore<TKey, TValue>> RetrieveObjectStore<TKey, TValue>(string objectStoreName)
        {
            ObjectStore<TKey,TValue> retval;

            if (this._JSTransactionReference != null)
            {
                IJSObjectReference objectStoreReference = await this._JSTransactionReference.InvokeAsync<IJSObjectReference>("RetrieveObjectStore", objectStoreName);

                retval = new ObjectStore<TKey, TValue>(objectStoreReference);
            }
            else
            {
                throw new Exception("Invalid State: Transaction is NULL.");
            }

            return retval;
        }
        #endregion

        #region Operations
        public async Task<bool> Commit()
        {
            bool retval;

            if (this._JSTransactionReference != null)
            {
                retval = await this._JSTransactionReference.InvokeAsync<bool>("Commit");
            }
            else
            {
                throw new Exception("Invalid State: Transaction is NULL.");
            }

            return retval;
        }
        public async Task<bool> Rollback()
        {
            bool retval;

            if (this._JSTransactionReference != null)
            {
                retval = await this._JSTransactionReference.InvokeAsync<bool>("Abort");
            }
            else
            {
                throw new Exception("Invalid State: Transaction is NULL.");
            }

            return retval;
        }
        #endregion
    }
}
