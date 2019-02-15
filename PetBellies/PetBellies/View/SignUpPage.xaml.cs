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
        private string successedRegistration = "";
        
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async Task signupButton_ClickedAsync(object sender, EventArgs e)
        {
            await Task.Run(() => {
                RegistrationAsync();
            });

            if (!String.IsNullOrEmpty(successedRegistration))
            {
                await DisplayAlert(English.Failed(), successedRegistration, English.OK());
                uploadActivity.IsRunning = false;
            }
            else
            {
                await DisplayAlert(English.Successful(), English.SuccessfulReg(), English.OK());
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
                FacebookId = null,
                ProfilePicture = null
            };

            string success = await GlobalVariables.signupPageViewModel.SignUpAsync(user);
            successedRegistration = success;
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
        
        void Handle_TappedAsync(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new TermsAndCondPage());
        }

        void Handle_Tapped2(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PrivaciPolicyPage());
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