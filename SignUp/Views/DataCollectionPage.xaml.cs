using SignUp.Core.ViewModels;
using Xamarin.Forms;

namespace SignUp.Views
{
    public partial class DataCollectionPage : ContentPage
    {
        public DataCollectionPage()
        {
            InitializeComponent();
            Title = "Data collection";
        }

        void OnGenderSelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (BindingContext is DataCollectionViewModel vm)
            {
                vm.Gender = PickerGender.ItemsSource[PickerGender.SelectedIndex] as string;
            }
        }

        void OnUniversitySelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (BindingContext is DataCollectionViewModel vm)
            {
                vm.University = PickerUniversity.ItemsSource[PickerGender.SelectedIndex] as string;
            }
        }

    }
}
