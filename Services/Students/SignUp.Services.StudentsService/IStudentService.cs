using System.Collections.Generic;
using System.Threading.Tasks;
using SignUp.Models.StudentModel;

namespace SignUp.Services.Students.StudentsService
{
    public interface IStudentService
    {   
        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns>The all students.</returns>
        Task<IEnumerable<StudentModel>> GetAllStudentsAsync();

        /// <summary>
        /// Gets the unsynched students.
        /// </summary>
        /// <returns>The unsynched students.</returns>
        Task<IEnumerable<StudentModel>> GetUnsynchedStudentsAsync();

        /// <summary>
        /// Adds the student.
        /// </summary>
        /// <returns>The student.</returns>
        /// <param name="student">Student.</param>
        Task<bool> AddStudentAsync(StudentModel student);

        /// <summary>
        /// Sets the student synched async.
        /// </summary>
        /// <returns>The student synched async.</returns>
        /// <param name="student">Student.</param>
        Task<bool> SetStudentSynchedAsync(StudentModel student);
    }
}
