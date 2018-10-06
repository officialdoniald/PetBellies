using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUpPage : ContentPage
	{
        private string asd = "";
        
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async Task signupButton_ClickedAsync(object sender, EventArgs e)
        {
            await Task.Run(() => {
                RegistrationAsync();
            });

            if (!String.IsNullOrEmpty(asd))
            {
                await DisplayAlert(English.Failed(), asd, English.OK());
                uploadActivity.IsRunning = false;
            }
            else
            {
                string sentMail = GlobalVariables.mailer.SendEmail(emailEntry.Text, string.Empty);

                await Navigation.PopToRootAsync();
            }

            signupButton.IsEnabled = true;
            uploadActivity.IsRunning = false;
        }

        private async Task RegistrationAsync()
        {

            Device.BeginInvokeOnMainThread(() => {
                signupButton.IsEnabled = false;
                uploadActivity.IsRunning = true;
            });

            User user = new User()
            {
                Email = emailEntry.Text,
                Password = pwEntry.Text,
                FirstName = firstnameEntry.Text,
                LastName = lastnameEntry.Text,
                FacebookId = "",
                ProfilePictureURL = ""
            };

            string success = await GlobalVariables.signupPageViewModel.SignUpAsync(user);
            asd = success;
        }

        void Handle_CompletedOnEmailEntry(object sender, System.EventArgs e)
        {
            pwEntry.Focus();
        }

        void Handle_CompletedOnLastNameEntry(object sender, System.EventArgs e)
        {
            firstnameEntry.Focus();
        }

        void Handle_CompletedOnFirstNameEntry(object sender, System.EventArgs e)
        {
            emailEntry.Focus();
        }

        async Task Handle_CompletedOnPasswordEntry(object sender, System.EventArgs e)
        {
            if (signupButton.IsEnabled)
            {
                await signupButton_ClickedAsync(this, new EventArgs());
            }
        }

        //private async void loginFacebookButton_Clicked(object sender, EventArgs e)
        //{
        //    DependencyService.Get<IClearCookies>().ClearAllWebAppCookies();

        //    await Navigation.PushAsync(new FacebookLogin.Views.FacebookProfileCsPage());
        //}

        async Task Handle_TappedAsync(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TermsAndCondPage());
        }

        async Task Handle_Tapped2(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new PrivaciPolicyPage());
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (imover13Switch.IsToggled)
            {
                signupButton.IsEnabled = true;
            }
            else
            {
                signupButton.IsEnabled = false;
            }
        }
    }
}