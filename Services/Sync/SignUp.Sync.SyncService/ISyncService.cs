using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SignUp.Models.StudentModel;

namespace SignUp.Sync.SyncService
{
    /// <summary>
    /// Sync service.
    /// </summary>
    public interface ISyncService
    {
        /// <summary>
        /// Syncs the students async.
        /// </summary>
        /// <returns>The students async.</returns>
        Task<IEnumerable<KeyValuePair<Guid, SyncStatus>>> SyncStudentsAsync();
    }
}
