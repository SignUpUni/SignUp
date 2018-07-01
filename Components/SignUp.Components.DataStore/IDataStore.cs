using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SignUp.Models.StudentModel;

namespace SignUp.Components.DataStore
{
    /// <summary>
    /// Data store.
    /// </summary>
    public interface IDataStore<T>
    {
        /// <summary>
        /// Add the specified entry.
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="entry">Entry.</param>
        Task<bool> AddAsync(T entry);

        /// <summary>
        /// Gets all the entries.
        /// </summary>
        /// <returns>The all.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Sets the sync status async.
        /// </summary>
        /// <returns>The sync status async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="syncStatus">Sync status.</param>
        Task<bool> SetSyncStatusAsync(Guid id, SyncStatus syncStatus);
    }

}
