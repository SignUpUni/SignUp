using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SignUp.Components.DataStore;
using SignUp.Models.StudentModel;

namespace Components.Support.DataStore.Touch
{
    /// <summary>
    /// Data store touch.
    /// </summary>
    public class DataStoreTouch : IDataStore<StudentModel>
    {
        readonly string _storeFile;
        List<StudentModel> _storeData;

        public DataStoreTouch()
        {
            _storeFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "store.json");
            _storeData = new List<StudentModel>();

            Initialise();
        }

        /// <summary>
        /// Add the specified entry.
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="entry">Entry.</param>
        public async Task<bool> AddAsync(StudentModel entry)
        {
            await Task.Run(() =>
            {
                int index = FindEntryIndex(entry);

                if (index < 0)
                {
                    _storeData.Add(entry);
                }
                else
                {
                    _storeData[index].FirstName = entry.FirstName;
                    _storeData[index].LastName = entry.LastName;
                    _storeData[index].Gender = entry.Gender;
                    _storeData[index].EmailAddress = entry.EmailAddress;
                    _storeData[index].University = entry.University;
                    _storeData[index].SyncStatus = entry.SyncStatus;
                }
                Save();
            });

            return true;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>The all.</returns>
        public async Task<IEnumerable<StudentModel>> GetAllAsync()
        {
            return await Task.FromResult<IEnumerable<StudentModel>>(_storeData);
        }

        /// <summary>
        /// Sets the sync status async.
        /// </summary>
        /// <returns>The sync status async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="syncStatus">Sync status.</param>
        public async Task<bool> SetSyncStatusAsync(Guid id, SyncStatus syncStatus)
        {
            return await Task.Run(() =>
            {
                var updated = false;

                int index = FindEntryIndex(id);

                if (index >= 0)
                {
                    updated = true;
                    _storeData[index].SyncStatus = syncStatus;
                    Save();
                }

                return updated;
            });
        }

        void Initialise()
        {
            if (File.Exists(_storeFile))
            {
                using (var file = File.OpenText(_storeFile))
                {
                    var storeData = file.ReadToEnd();

                    if (!string.IsNullOrWhiteSpace(storeData))
                    {
                        _storeData = JsonConvert.DeserializeObject<List<StudentModel>>(storeData);
                    }
                }
            }
        }

        void Save()
        {
            var storeDataSerialised = JsonConvert.SerializeObject(_storeData);
            File.WriteAllText(_storeFile, storeDataSerialised);
        }

        int FindEntryIndex(StudentModel entry)
        {
            return _storeData.FindIndex((student) => student.Id == entry.Id);
        }

        int FindEntryIndex(Guid id)
        {
            return _storeData.FindIndex((student) => student.Id == id);
        }
    }
}
