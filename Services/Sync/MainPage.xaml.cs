using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using SignUp.Core.ViewModels;

namespace SignUp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void OnButtonDataCollectionTapped(object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(
                async () => await
                    Application.Current.MainPage.Navigation.PushAsync(
                        new DataCollectionPage
                        {
                            BindingContext = new DataCollectionViewModel()
                        }
                   )
            );
        }
    }
}
