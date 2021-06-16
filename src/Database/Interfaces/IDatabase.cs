using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorIndexedDbSample.Interfaces
{
    public interface IDatabase
    {
        #region Properties
        string Name { get; }
        ulong Version { get; }
        public bool IsOpen { get; }
        #endregion

        #region Operations
        Task<IReadOnlyList<string>> RetrieveObjectStoreNames();
        Task<IDatabaseTransaction> CreateTransaction(string objectStoreName);
        #endregion
    }
}
