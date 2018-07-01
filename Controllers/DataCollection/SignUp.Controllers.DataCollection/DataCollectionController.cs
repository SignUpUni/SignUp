using System.Threading.Tasks;
using SignUp.Models.StudentModel;
using SignUp.Services.Students.StudentsService;

namespace SignUp.Controllers.DataCollection
{
    /// <summary>
    /// Data collection controller.
    /// </summary>
    public class DataCollectionController : IDataCollectionController
    {
        readonly IStudentService _studentService;

        public DataCollectionController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Saves the student.
        /// </summary>
        /// <returns>The student.</returns>
        /// <param name="studentModel">Student model.</param>
        public async Task<bool> SaveStudentAsync(StudentModel studentModel)
        {
            //TODO: check if the email is valid
            return await _studentService.AddStudentAsync(studentModel);
        }
    }
}
