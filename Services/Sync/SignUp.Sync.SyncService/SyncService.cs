using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignUp.Models.StudentModel;
using SignUp.Services.Students.StudentsService;

namespace SignUp.Sync.SyncService
{
    /// <summary>
    /// Sync service.
    /// </summary>
    public class SyncService : ISyncService
    {
        readonly IStudentService _studentService;

        public SyncService(IStudentService studentService)
        {
            _studentService = studentService;        
        }

        /// <summary>
        /// Syncs the students.
        /// </summary>
        /// <returns>The students.</returns>
        public async Task<IEnumerable<KeyValuePair<Guid, SyncStatus>>> SyncStudentsAsync()
        {
            var synchedStats = new List<KeyValuePair<Guid, SyncStatus>>();

            var unsynchedStudents = await _studentService.GetUnsynchedStudentsAsync();

            if (unsynchedStudents.Any())
            {
                foreach (var unsynchedStudent in unsynchedStudents)
                {
                    if (await _studentService.SetStudentSynchedAsync(unsynchedStudent))
                    {
                        synchedStats.Add(new KeyValuePair<Guid, SyncStatus>(unsynchedStudent.Id, SyncStatus.Synched));
                    }
                    else
                    {
                        synchedStats.Add(new KeyValuePair<Guid, SyncStatus>(unsynchedStudent.Id, SyncStatus.NotSynched));
                    }
                }
            }

            return synchedStats;
        }
    }
}
