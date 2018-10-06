using PetBellies.BLL.FileStoreAndLoad;
using PetBellies.BLL.Helper;
using Plugin.Connectivity;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        private bool wasNotConn = false;

        public LoginPage()
        {
            InitializeComponent();

            CrossConnectivity.Current.ConnectivityChanged += async (sender, args) =>
            {
                if (!CrossConnectivity.Current.IsConnected && !wasNotConn)
                {
                    wasNotConn = true;

                    await Navigation.PushModalAsync(new NoConnection(true));
                }
                else
                {
                    wasNotConn = false;
                }
            };

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void loginButton_Clicked(object sender, EventArgs e)
        {
            loginButton.IsEnabled = false;

            string success = GlobalVariables.loginPageViewModel.Login(emailEntry.Text, pwEntry.Text);

            if (!String.IsNullOrEmpty(success))
            {
                Device.BeginInvokeOnMainThread(() => loginActivator.IsRunning = false);

                DisplayAlert(English.Failed(), success, English.OK());
            }
            else
            {
                FileStoreAndLoading.InsertToFile(GlobalVariables.logintxt, emailEntry.Text);

                Navigation.PushModalAsync(new JustActivityIndicator("login"));
            }

            loginButton.IsEnabled = true;
        }

        private async void signUpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
        
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
        }

        void Handle_CompletedOnPassword(object sender, System.EventArgs e)
        {
            loginButton_Clicked(this, new EventArgs());
        }

        void Handle_CompletedOnEmail(object sender, System.EventArgs e)
        {
            pwEntry.Focus();
        }
    }
}