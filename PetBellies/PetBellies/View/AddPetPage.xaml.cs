using PetBellies.BLL.Helper;
using PetBellies.Model;
using Plugin.Media;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddPetPage : ContentPage
	{
        private bool addedPhoto = false;
        private Stream f;

        public AddPetPage()
        {
            InitializeComponent();

            ageDatePicker.MaximumDate = DateTime.Now;
        }

        private void addPetButton_ClickedAsync(object sender, EventArgs e)
        {
            Task.Run(()=> {
                DisableOrEnableButtons(false);

                bool isChecked = shelterpetSwitch.IsToggled;

                int isCheckedToInt = 1;

                if (isChecked) isCheckedToInt = 0;
                
                Pet pet = new Pet()
                {
                    Name = nameEntry.Text,
                    Age = ageDatePicker.Date,
                    PetType = typeEntry.Text,
                    HaveAnOwner = isCheckedToInt
                };

                string success = GlobalVariables.addpetFragmentViewModel.AddPetAsync(addedPhoto, f, pet);

                if (!string.IsNullOrEmpty(success))
                {
                    Device.BeginInvokeOnMainThread(() => {
                        DisplayAlert(English.Failed(), success, English.OK());
                    });
                }
                else
                {
                    addedPhoto = false;

                    Device.BeginInvokeOnMainThread(() => {
                        Navigation.PopAsync();
                    });
                }

                DisableOrEnableButtons(true);
            });
        }

        private void DisableOrEnableButtons(bool enable)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                addPetActivator.IsRunning = !enable;
                galleryButton.IsEnabled = enable;
                addPetButton.IsEnabled = enable;
            });
        }

        private async void galleryButton_ClickedAsync(object sender, EventArgs args)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert(English.Failed(), English.NoPicking(), English.OK());
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            
            if (file == null) return;

            f = file.GetStream();

            GlobalVariables.SourceSelectedImageFromGallery = file.Path;
            GlobalVariables.SelectedImageFromGallery = f;

            addedPhoto = true;

            profilePictureImage.Source = ImageSource.FromStream(() => f);
        }

        void Handle_CompletedOnNameEntry(object sender, System.EventArgs e)
        {
            ageDatePicker.Focus();
        }

        void Handle_CompletedOnAgeEntry(object sender, System.EventArgs e)
        {
            typeEntry.Focus();
        }

        void Handle_CompletedOnTypeEntry(object sender, System.EventArgs e)
        {
            shelterpetSwitch.Focus();
        }
    }
}