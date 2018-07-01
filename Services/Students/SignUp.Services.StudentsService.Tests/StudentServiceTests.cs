using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SignUp.Models.StudentModel;
using SignUp.Repositories.StudentRepository;
using SignUp.Services.Students.StudentsService;

namespace SignUp.Services.StudentsService.Tests
{
    [TestFixture]
    public class StudentServiceTests
    {
        StudentService _studentRepository;
        Mock<IStudentRepository> _studentRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _studentRepositoryMock = new Mock<IStudentRepository>(MockBehavior.Strict);
            _studentRepository = new StudentService(_studentRepositoryMock.Object);
        }

        #region AddStudent

        [Test]
        public async Task AddStudent_WhenSuccessfull_ReturnsTrue()
        {
            _studentRepositoryMock.Setup(mn => mn.AddStudentAsync(It.IsAny<StudentModel>())).ReturnsAsync(true);

            Assert.IsTrue(await _studentRepository.AddStudentAsync(new StudentModel()));
        }

        [Test]
        public async Task AddStudent_WhenNotSuccessfull_ReturnsFalse()
        {
            _studentRepositoryMock.Setup(mn => mn.AddStudentAsync(It.IsAny<StudentModel>())).ReturnsAsync(false);

            Assert.IsFalse(await _studentRepository.AddStudentAsync(new StudentModel()));
        }

        [Test]
        public async Task AddStudent_WhenCalled_CallsRepositoryAddStudent()
        {
            var studentModel = new StudentModel();
            _studentRepositoryMock.Setup(mn => mn.AddStudentAsync(It.IsAny<StudentModel>())).ReturnsAsync(true);
            await _studentRepository.AddStudentAsync(studentModel);
            _studentRepositoryMock.Verify(mn => mn.AddStudentAsync(studentModel), Times.Once);
        }

        #endregion

        #region GetAllStudents

        [Test]
        public async Task GetAllStudents_WhenTheRepositoryIsEmpty_ReturnAnEmptyList()
        {
            _studentRepositoryMock.Setup(mn => mn.GetAllStudentsAsync()).ReturnsAsync(new List<StudentModel>());

            var result = await _studentRepository.GetAllStudentsAsync();

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public async Task GetAllStudents_WhenTheRepositoryIsNotEmpty_ReturnNotAnEmptyList()
        {
            _studentRepositoryMock.Setup(mn => mn.GetAllStudentsAsync()).ReturnsAsync(new List<StudentModel>{
                new StudentModel()
            });

            var result = await _studentRepository.GetAllStudentsAsync();

            Assert.AreEqual(1, result.Count());
        }

        #endregion

        #region GetUnsynchedStudents

        [Test]
        public async Task GetUnsynchedStudents_WhenThereAreNoUnsynchedStudents_ReturnAnEmptyList()
        {
            _studentRepositoryMock.Setup(mn => mn.GetAllStudentsAsync()).ReturnsAsync(new List<StudentModel>());

            var result = await _studentRepository.GetUnsynchedStudentsAsync();

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public async Task GetUnsynchedStudents_WhenThereAreUnsynchedStudents_ReturnsOnlyTheUnsynchedStudents()
        {
            _studentRepositoryMock.Setup(mn => mn.GetAllStudentsAsync()).ReturnsAsync(new List<StudentModel>()
            {
                new StudentModel
                {
                    SyncStatus=SyncStatus.Synched
                },
                new StudentModel
                {
                    SyncStatus=SyncStatus.NotSynched
                }
            });

            var result = await _studentRepository.GetUnsynchedStudentsAsync();

            Assert.AreEqual(1, result.Count());
        }
        #endregion
    }
}
