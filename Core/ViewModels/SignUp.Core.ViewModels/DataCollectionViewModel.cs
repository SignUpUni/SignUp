using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SignUp.Controllers.DataCollection;
using SignUp.Models.StudentModel;
using Xamarin.Forms;

namespace SignUp.Core.ViewModels
{
    /// <summary>
    /// Data collection view model.
    /// </summary>
    public class DataCollectionViewModel : BaseViewModel
    {
        readonly IDataCollectionController _dataCollectionController;
        string _firstname;
        string _lastName;
        string _gender;
        string _emailAddress;
        string _university;

        public DataCollectionViewModel(IDataCollectionController dataCollectionController)
        {
            _dataCollectionController = dataCollectionController;
            SaveDataCommand = new Command(async () => await ExecuteSaveDataCommand());
        }

        /// <summary>
        /// Gets the save data command.
        /// </summary>
        /// <value>The save data command.</value>
        public ICommand SaveDataCommand { get; private set; }

        #region Bindables

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get => _firstname;
            set
            {
                SetProperty(ref _firstname, value);
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get => _lastName;
            set
            {
                SetProperty(ref _lastName, value);
            }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public string Gender
        {
            get => _gender;
            set
            {
                SetProperty(ref _gender, value);
            }
        }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                SetProperty(ref _emailAddress, value);
            }
        }

        public string University
        {
            get => _university;
            set
            {
                SetProperty(ref _university, value);
            }
        }

        #endregion

        async Task ExecuteSaveDataCommand()
        {
            bool saved = false;
            bool invalidData = false;

            try
            {
                saved = await _dataCollectionController.SaveStudentAsync(
                    new StudentModel
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        Gender = Gender,
                        EmailAddress = EmailAddress,
                        University = University
                    });
            }
            catch (ArgumentException argumentException)
            {
                //TODO: replace with a app logger
                Console.WriteLine(argumentException);
                invalidData = true;
            }

            if (saved)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                if (invalidData)
                {
                    //TODO: throw specific errors
                    await Application.Current.MainPage.DisplayAlert("Warning",
                        "Please make sure you have filled all the fields.", "Ok");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Cannot save student.", "Ok");
                }
            }
        }
    }
}
