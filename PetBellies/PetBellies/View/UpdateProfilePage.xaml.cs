using PetBellies.BLL.Helper;
using Plugin.Media;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateProfilePage : ContentPage
	{
        public UpdateProfilePage()
        {
            InitializeComponent();

            lastnameEntry.Placeholder = GlobalVariables.ActualUser.LastName;
            firstnameEntry.Placeholder = GlobalVariables.ActualUser.FirstName;
            emailEntry.Placeholder = GlobalVariables.ActualUser.Email;
            
            profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(GlobalVariables.ActualUser.ProfilePictureURL));
        }

        private async void updateMyProfileButton_ClickedAsync(object sender, EventArgs e)
        {
            updateMyProfileButton.IsEnabled = false;

            string success = GlobalVariables.updateProfileFragmentViewModel.UpdateProfile(firstnameEntry.Text, lastnameEntry.Text);

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(), success, English.OK());
            }
            else
            {
                GlobalVariables.IsUpdatedMyProfile = true;

                await Navigation.PopAsync();
            }

            updateMyProfileButton.IsEnabled = true;
        }

        private async void changeEmailButton_ClickedAsync(object sender, EventArgs e)
        {
            changeEmailButton.IsEnabled = false;

            string success = await GlobalVariables.updateProfileFragmentViewModel.UpdateEmailAsync(emailEntry.Text);

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(), success, English.OK());
            }
            else
            {
                await Navigation.PopAsync();
            }

            changeEmailButton.IsEnabled = true;
        }

        private async Task changepwButton_ClickedAsync(object sender, EventArgs e)
        {
            changepwButton.IsEnabled = false;

            string success = GlobalVariables.updateProfileFragmentViewModel.UpdatePassword(pwEntry.Text, newpwEntry.Text);

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(), success, English.OK());
            }
            else
            {
                await Navigation.PopAsync();
            }

            changepwButton.IsEnabled = true;
        }

        private async Task changeProfilePictureButton_ClickedAsync(object sender, EventArgs e)
        {
            changeProfilePictureButton.IsEnabled = false;

            string success = await GlobalVariables.updateProfileFragmentViewModel.UpdateProfilePicture(GlobalVariables.pathf, GlobalVariables.f);

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(), success, English.OK());
            }
            else
            {
                await Navigation.PopAsync();
            }

            changeProfilePictureButton.IsEnabled = true;
        }

        private async Task galleryButton_ClickedAsync(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert(English.Failed(), English.NoPicking(), English.OK());
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            GlobalVariables.mediaFile = file;

            if (file == null) return;

            GlobalVariables.f = file.GetStream();
            GlobalVariables.pathf = file.Path;

            profilePictureImage.Source = ImageSource.FromStream(() => file.GetStream());
        }

        void Handle_CompletedOnOldPWEntry(object sender, System.EventArgs e)
        {
            newpwEntry.Focus();
        }

        async Task Handle_CompletedOnNewPWEntry(object sender, System.EventArgs e)
        {
            await changepwButton_ClickedAsync(this, new EventArgs());
        }

        //private async void changeFaceookButton_ClickedAsync(object sender, EventArgs e)
        //{
        //    changeFaceookButton.IsEnabled = false;

        //    DependencyService.Get<IClearCookies>().ClearAllWebAppCookies();

        //    await Navigation.PushAsync(new FacebookLogin.Views.FacebookProfileCsPage());

        //    changeFaceookButton.IsEnabled = true;
        //}

    }
}