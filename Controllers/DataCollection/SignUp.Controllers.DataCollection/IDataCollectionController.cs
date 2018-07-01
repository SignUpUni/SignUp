using System.Threading.Tasks;
using SignUp.Models.StudentModel;

namespace SignUp.Controllers.DataCollection
{
    /// <summary>
    /// Data collection controller.
    /// </summary>
    public interface IDataCollectionController
    {
        /// <summary>
        /// Saves the student.
        /// </summary>
        /// <returns>The student.</returns>
        /// <param name="studentModel">Student model.</param>
        Task<bool> SaveStudentAsync(StudentModel studentModel);
    }
}
