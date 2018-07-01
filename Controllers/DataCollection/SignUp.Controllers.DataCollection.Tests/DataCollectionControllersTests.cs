using System;
using NUnit.Framework;
using Moq;
using SignUp.Services.Students.StudentsService;
using SignUp.Models.StudentModel;
using System.Threading.Tasks;

namespace SignUp.Controllers.DataCollection.Tests
{
    [TestFixture]
    public class DataCollectionControllersTests
    {
        DataCollectionController _dataCollectionController;
        Mock<IStudentService> _studentServiceMock;

        [SetUp]
        public void SetUp()
        {
            _studentServiceMock = new Mock<IStudentService>(MockBehavior.Strict);

            _dataCollectionController = new DataCollectionController(_studentServiceMock.Object);
        }

        [Test]
        public async Task SaveStudent_WhenSuccessfull_ReturnsTrue()
        {
            _studentServiceMock.Setup(mn => mn.AddStudentAsync(It.IsAny<StudentModel>())).ReturnsAsync(true);
            Assert.IsTrue(await _dataCollectionController.SaveStudentAsync(new StudentModel()));
        }

        [Test]
        public async Task SaveStudent_WhenSuccessfull_ReturnsFalse()
        {
            _studentServiceMock.Setup(mn => mn.AddStudentAsync(It.IsAny<StudentModel>())).ReturnsAsync(false);
            Assert.IsFalse(await _dataCollectionController.SaveStudentAsync(new StudentModel()));
        }
    }
}
