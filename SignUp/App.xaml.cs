using SignUp.Components.DataStore;
using SignUp.Models.StudentModel;
using SignUp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SignUp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        public App(IDataStore<StudentModel> dataStore)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage(dataStore));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
