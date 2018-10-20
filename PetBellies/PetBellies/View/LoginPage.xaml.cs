using PetBellies.BLL.FileStoreAndLoad;
using PetBellies.BLL.Helper;
using Plugin.Connectivity;
using System;
using System.Threading.Tasks;
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
            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    loginButton.IsEnabled = false;
                    loginActivator.IsRunning = true;
                });

                string success = GlobalVariables.loginPageViewModel.Login(emailEntry.Text, pwEntry.Text);

                if (!String.IsNullOrEmpty(success))
                {
                    Device.BeginInvokeOnMainThread(() =>
                        DisplayAlert(English.Failed(), success, English.OK())
                    );
                }
                else
                {
                    FileStoreAndLoading.InsertToFile(GlobalVariables.logintxt, emailEntry.Text);

                    Device.BeginInvokeOnMainThread(() =>
                        Navigation.PushModalAsync(new JustActivityIndicator("login"))
                    );
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    loginButton.IsEnabled = true;
                    loginActivator.IsRunning = false;
                });
            });
        }

        private void signUpButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
        
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPasswordPage());
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