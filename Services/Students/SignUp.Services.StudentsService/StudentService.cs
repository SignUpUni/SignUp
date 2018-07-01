using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignUp.Models.StudentModel;
using SignUp.Repositories.StudentRepository;

namespace SignUp.Services.Students.StudentsService
{
    /// <summary>
    /// Student service.
    /// </summary>
    public class StudentService : IStudentService
    {
        readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Adds the student.
        /// </summary>
        /// <returns>The student.</returns>
        /// <param name="student">Student.</param>
        public async Task<bool> AddStudentAsync(StudentModel student)
        {
            return await _studentRepository.AddStudentAsync(student);
        }

        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns>The all students.</returns>
        public async Task<IEnumerable<StudentModel>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }

        /// <summary>
        /// Gets the unsynched students.
        /// </summary>
        /// <returns>The unsynched students.</returns>
        public async Task<IEnumerable<StudentModel>> GetUnsynchedStudentsAsync()
        {
            var result = await _studentRepository.GetAllStudentsAsync();

            return result?.Where((student) => student.SyncStatus == SyncStatus.NotSynched);
        }

        /// <summary>
        /// Sets the student synched async.
        /// </summary>
        /// <returns>The student synched async.</returns>
        /// <param name="student">Student.</param>
        public async Task<bool> SetStudentSynchedAsync(StudentModel student)
        {
            return await _studentRepository.UpdateStudentSyncStatusAsync(student.Id, SyncStatus.Synched);
        }
    }
}
