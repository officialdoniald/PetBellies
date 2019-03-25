using PetBellies.BLL.Helper;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForgotPasswordPage : ContentPage
	{
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void sendNewPassword_ClickedAsync(object sender, EventArgs e)
        {
            var success = await GlobalVariables.forgotPasswordPageViewModel.SendEmailAsync(emailEntry.Text);

            if (string.IsNullOrEmpty(success))
            {
                await Navigation.PopToRootAsync();
            }
            else
            {
                await DisplayAlert(English.Failed(), success, English.OK());
            }
        }

        async void Handle_Completed(object sender, System.EventArgs e)
        {
            sendNewPassword_ClickedAsync(this, new EventArgs());
        }
    }
}