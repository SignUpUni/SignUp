using System.Collections.Generic;
using System.Threading.Tasks;
using SignUp.Models.StudentModel;
using System;

namespace SignUp.Repositories.StudentRepository
{
    /// <summary>
    /// Student repository.
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns>The all students.</returns>
        Task<IEnumerable<StudentModel>> GetAllStudentsAsync();

        /// <summary>
        /// Adds the student.
        /// </summary>
        /// <returns>The student.</returns>
        /// <param name="student">Student.</param>
        Task<bool> AddStudentAsync(StudentModel student);

        /// <summary>
        /// Updates the student sync status.
        /// </summary>
        /// <returns>The student sync status.</returns>
        /// <param name="status">Status.</param>
        Task<bool> UpdateStudentSyncStatusAsync(Guid id, SyncStatus status);
    }
}
