using PetBellies.BLL.Helper;
using Plugin.Media;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateProfilePage : ContentPage
    {
        private bool addedPhoto = false;
        private Stream f;

        public UpdateProfilePage()
        {
            InitializeComponent();

            Device.BeginInvokeOnMainThread(() => {
                lastnameEntry.Placeholder = GlobalVariables.ActualUser.LastName;
                firstnameEntry.Placeholder = GlobalVariables.ActualUser.FirstName;
                emailEntry.Placeholder = GlobalVariables.ActualUser.Email;
                profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(GlobalVariables.ActualUser.ProfilePicture));
            });
        }

        private void updateMyProfileButton_ClickedAsync(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                DisableOrEnableButtons(false);

                string success = GlobalVariables.updateProfileFragmentViewModel.UpdateProfile(firstnameEntry.Text, lastnameEntry.Text);

                if (!String.IsNullOrEmpty(success))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert(English.Failed(), success, English.OK());
                    });
                }
                else
                {
                    GlobalVariables.IsUpdatedMyProfile = true;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert(English.Successful(), English.Successful(), English.OK());
                    });
                }

                DisableOrEnableButtons(true);
            });
        }

        private void changeEmailButton_ClickedAsync(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                DisableOrEnableButtons(false);

                string success = await GlobalVariables.updateProfileFragmentViewModel.UpdateEmailAsync(emailEntry.Text);

                if (!String.IsNullOrEmpty(success))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert(English.Failed(), success, English.OK());
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert(English.Successful(), English.Successful(), English.OK());
                    });
                }

                DisableOrEnableButtons(true);
            });
        }

        private void changepwButton_ClickedAsync(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                DisableOrEnableButtons(false);

                string success = GlobalVariables.updateProfileFragmentViewModel.UpdatePassword(pwEntry.Text, newpwEntry.Text);

                if (!String.IsNullOrEmpty(success))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert(English.Failed(), success, English.OK());
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert(English.Successful(), English.Successful(), English.OK());
                    });
                }

                DisableOrEnableButtons(true);
            });
        }

        private void changeProfilePictureButton_ClickedAsync(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                DisableOrEnableButtons(false);

                string success = GlobalVariables.updateProfileFragmentViewModel.UpdateProfilePicture(addedPhoto);

                if (!string.IsNullOrEmpty(success))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert(English.Failed(), success, English.OK());
                    });
                }
                else
                {
                    addedPhoto = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert(English.Successful(), English.Successful(), English.OK());
                    });
                }

                DisableOrEnableButtons(true);
            });
        }

        private void DisableOrEnableButtons(bool enable)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                updateProfileActivator.IsRunning = !enable;
                galleryButton.IsEnabled = enable;
                changeProfilePictureButton.IsEnabled = enable;
                changepwButton.IsEnabled = enable;
                changeEmailButton.IsEnabled = enable;
                updateMyProfileButton.IsEnabled = enable;
            });
        }

        private async Task galleryButton_ClickedAsync(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert(English.Failed(), English.NoPicking(), English.OK());
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            
            if (file == null) return;
            
            addedPhoto = true;
            f = file.GetStream();
            GlobalVariables.SourceSelectedImageFromGallery = file.Path;
            GlobalVariables.SelectedImageFromGallery = f;

            profilePictureImage.Source = ImageSource.FromStream(() => f);
        }

        void Handle_CompletedOnOldPWEntry(object sender, System.EventArgs e)
        {
            newpwEntry.Focus();
        }

        void Handle_CompletedOnNewPWEntry(object sender, System.EventArgs e)
        {
            changepwButton_ClickedAsync(this, new EventArgs());
        }
    }
}