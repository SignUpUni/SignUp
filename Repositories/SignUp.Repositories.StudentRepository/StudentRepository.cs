using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SignUp.Components.DataStore;
using SignUp.Models.StudentModel;

namespace SignUp.Repositories.StudentRepository
{
    /// <summary>
    /// Student repository.
    /// </summary>
    public class StudentRepository : IStudentRepository
    {
        readonly IDataStore<StudentModel> _dataStore;

        public StudentRepository(IDataStore<StudentModel> dataStore)
        {
            _dataStore = dataStore;
        }

        /// <summary>
        /// Adds the student.
        /// </summary>
        /// <returns>The student.</returns>
        /// <param name="student">Student.</param>
        public async Task<bool> AddStudentAsync(StudentModel student)
        {
            ValidateStudentModel(student);
            return await _dataStore.AddAsync(student);
        }

        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns>The all students.</returns>
        public async Task<IEnumerable<StudentModel>> GetAllStudentsAsync()
        {
            return await _dataStore.GetAllAsync();
        }

        /// <summary>
        /// Updates the student sync status async.
        /// </summary>
        /// <returns>The student sync status async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="status">Status.</param>
        public async Task<bool> UpdateStudentSyncStatusAsync(Guid id, SyncStatus status)
        {
            return await _dataStore.SetSyncStatusAsync(id, status);
        }

        void ValidateStudentModel(StudentModel student)
        {
            if (student == null)
            {
                throw new ArgumentException(nameof(student));
            }

            if (student.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(student.Id));
            }

            if (string.IsNullOrWhiteSpace(student.FirstName))
            {
                throw new ArgumentException(nameof(student.FirstName));
            }

            if (string.IsNullOrWhiteSpace(student.LastName))
            {
                throw new ArgumentException(nameof(student.LastName));
            }

            if (string.IsNullOrWhiteSpace(student.Gender))
            {
                throw new ArgumentException(nameof(student.Gender));
            }

            if (string.IsNullOrWhiteSpace(student.EmailAddress))
            {
                throw new ArgumentException(nameof(student.EmailAddress));
            }

            if (string.IsNullOrWhiteSpace(student.University))
            {
                throw new ArgumentException(nameof(student.University));
            }
        }
    }
}
