using System.ComponentModel;
using NUnit.Framework;

namespace SignUp.Core.ViewModels.Tests
{
    [TestFixture]
    public class BaseViewModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BaseViewModel_WhenDefined_ImplementsINotifyPropertyChanged()
        {
            Assert.IsInstanceOf(typeof(INotifyPropertyChanged), new TestBaseViewModel());
        }

        [Test]
        public void RaisePropertyChanged_WhenSet_CallsTheEventHandler()
        {
            var testBaseViewModel = new TestBaseViewModel
            {
                TestProperty = true
            };

            Assert.IsTrue(testBaseViewModel.OnPropertyChangedCalled);
        }

        class TestBaseViewModel : BaseViewModel
        {
            bool _testProperty;

            public bool TestProperty
            {
                get => _testProperty;
                set => SetProperty(ref _testProperty, value);
            }

            public bool OnPropertyChangedCalled
            {
                get;
                set;
            }

            public TestBaseViewModel()
            {
                PropertyChanged += OnPropertyChanged;
            }

            void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                OnPropertyChangedCalled = true;
            }
        }

    }

}
