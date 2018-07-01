using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SignUp.Components.DataStore;
using SignUp.Models.StudentModel;

namespace SignUp.Repositories.StudentRepository.Tests
{
    [TestFixture]
    public class StudentRepositoryTests
    {
        const string TestStudentFirstName = "TestStudentName";
        const string TestStudentLastName = "TestStudentLastName";
        const string TestStudentGender = "TestStudentGender";
        const string TestStudentEmailAddress = "TestStudentEmailAddress";
        const string TestStudentUniversity = "TestStudentUniversity";

        static IEnumerable<TestCaseData> InvalidParameterData()
        {
            yield return new TestCaseData(GenerateTestStudentModel()).
                SetName("AddStudent_WhenTheFirstNameIsInvalid_ThrowsException");

            yield return new TestCaseData(GenerateTestStudentModel(TestStudentFirstName)).
                SetName("AddStudent_WhenTheLastNameIsInvalid_ThrowsException");

            yield return new TestCaseData(GenerateTestStudentModel(TestStudentFirstName, TestStudentLastName)).
                SetName("AddStudent_WhenTheGenderIsInvalid_ThrowsException");

            yield return new TestCaseData(
                GenerateTestStudentModel(TestStudentFirstName, TestStudentLastName, TestStudentGender)).
                SetName("AddStudent_WhenTheEmailAddressIsInvalid_ThrowsException");

            yield return new TestCaseData(
                GenerateTestStudentModel(TestStudentFirstName, TestStudentLastName, TestStudentGender,
                                         TestStudentEmailAddress)).
                SetName("AddStudent_WhenTheUniversityIsInvalid_ThrowsException");
        }

        StudentRepository _studentRepository;
        Mock<IDataStore<StudentModel>> _dataStoreMock;

        [SetUp]
        public void Setup()
        {
            _dataStoreMock = new Mock<IDataStore<StudentModel>>(MockBehavior.Strict);
            _dataStoreMock.Setup(mn => mn.AddAsync(It.IsAny<StudentModel>())).ReturnsAsync(true);
            _dataStoreMock.Setup(mn => mn.GetAllAsync()).ReturnsAsync(new List<StudentModel>());

            _studentRepository = new StudentRepository(_dataStoreMock.Object);
        }

        #region AddStudentTests
        [Test]
        public void AddStudent_WhenTheParameterIsNull_ThrowsException()
        {
            Assert.ThrowsAsync<ArgumentException>(
                async () => await _studentRepository.AddStudentAsync(null));
        }

        [TestCaseSource(nameof(InvalidParameterData))]
        public void AddStudent_WhenTheParameterIsInvalid_ThrowsException(StudentModel model)
        {
            Assert.ThrowsAsync<ArgumentException>(
                async () => await _studentRepository.AddStudentAsync(model));
        }

        [Test]
        public async Task AddStudent_WhenTheParameterIsValid_ReturnsTrue()
        {
            Assert.IsTrue(await _studentRepository.AddStudentAsync(GenerateValidStudentModel()));
        }

        [Test]
        public async Task AddStudent_WhenTheParameterIsValid_IsCallingTheStoreAddStudentMethod()
        {
            var studentModel = GenerateValidStudentModel();

            await _studentRepository.AddStudentAsync(studentModel);

            _dataStoreMock.Verify(mn => mn.AddAsync(studentModel), Times.Once);
        }

        #endregion

        #region GetAllTests

        [Test]
        public async Task GetAll_WhenTheStoreIsEmpty_ReturnsAnEmptyList()
        {
            var result = await _studentRepository.GetAllStudentsAsync();
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public async Task GetAll_WhenTheStoreIsNotEmpty_ReturnsTheExactNumberOfElements()
        {
            _dataStoreMock.Setup(mn => mn.GetAllAsync()).
                ReturnsAsync(new List<StudentModel> { GenerateValidStudentModel() });

            var result = await _studentRepository.GetAllStudentsAsync();

            Assert.AreEqual(1, result.Count());
        }

        #endregion

        #region Internals
        static StudentModel GenerateTestStudentModel(string firstName = null, string lastName = null, string gender = null,
            string emailAddress = null, string university = null)
        {
            return new StudentModel
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                EmailAddress = emailAddress,
                University = university
            };
        }

        StudentModel GenerateValidStudentModel()
        {
            return new StudentModel
            {
                FirstName = TestStudentFirstName,
                LastName = TestStudentLastName,
                Gender = TestStudentGender,
                EmailAddress = TestStudentEmailAddress,
                University = TestStudentUniversity
            };
        }

        #endregion
    }
}
