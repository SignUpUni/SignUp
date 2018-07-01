using System.Linq;
using SignUp.Components.DataStore;
using SignUp.Controllers.DataCollection;
using SignUp.Core.ViewModels;
using SignUp.Models.StudentModel;
using SignUp.Repositories.StudentRepository;
using SignUp.Services.Students.StudentsService;
using SignUp.Sync.SyncService;
using Xamarin.Forms;

namespace SignUp.Views
{
    public partial class MainPage : ContentPage
    {
        readonly IDataStore<StudentModel> _dataStore;
        readonly StudentRepository _studentRepository;
        readonly StudentService _studentService;
        readonly SyncService _syncService;
        readonly DataCollectionController _dataCollectionController;

        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(IDataStore<StudentModel> dataStore) : base()
        {
            InitializeComponent();

            _dataStore = dataStore;

            _studentRepository = new StudentRepository(_dataStore);
            _studentService = new StudentService(_studentRepository);
            _syncService = new SyncService(_studentService);
            _dataCollectionController = new DataCollectionController(_studentService);

            Title = "SignUp";
        }

        void OnButtonDataCollectionTapped(object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(
                async () => await
                    Application.Current.MainPage.Navigation.PushAsync(
                        new DataCollectionPage
                        {
                            BindingContext = new DataCollectionViewModel(_dataCollectionController)
                        }
                   )
            );
        }

        async void OnButtonSyncTapped(object sender, System.EventArgs e)
        {
            var result = await _syncService.SyncStudentsAsync();

            if (result?.Any() == true)
            {
                LabelStatus.Text = $"{result.Count((record) => record.Value == SyncStatus.Synched)} of ";
                LabelStatus.Text = $"{result.Count()} records were synched";
                StackStatus.IsVisible = true;
            }
        }

        void OnButtonCloseAlertTapped(object sender, System.EventArgs e)
        {
            StackStatus.IsVisible = false;
            LabelStatus.Text = "";
        }
    }
}