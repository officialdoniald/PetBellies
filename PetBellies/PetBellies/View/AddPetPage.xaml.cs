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
        }

        private async Task addPetButton_ClickedAsync(object sender, EventArgs e)
        {
            uploadActivity.IsRunning = true;
            addPetButton.IsEnabled = false;
            galleryButton.IsEnabled = false;

            addedPhoto = true;

            bool isChecked = shelterpetSwitch.IsToggled;

            int isCheckedToInt = 1;

            if (isChecked) isCheckedToInt = 0;

            int age;

            try
            {
                age = Convert.ToInt32(ageEntry.Text);
            }
            catch (Exception)
            {
                await DisplayAlert(English.Failed(), English.YouHaveToFillAllEntries(), English.OK());

                return;
            }

            Pet pet = new Pet()
            {
                Name = nameEntry.Text,
                Age = age,
                PetType = typeEntry.Text,
                HaveAnOwner = isCheckedToInt
            };

            string success = GlobalVariables.addpetFragmentViewModel.AddPetAsync(addedPhoto, f, pet);

            if (!string.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(), success, English.OK());
            }
            else
            {
                addedPhoto = false;
                
                await Navigation.PopAsync();
            }

            galleryButton.IsEnabled = true;
            addPetButton.IsEnabled = true;
            uploadActivity.IsRunning = false;
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

            f = file.GetStream();

            GlobalVariables.sstream = file.Path;
            GlobalVariables.stream = f;

            profilePictureImage.Source = ImageSource.FromStream(() => f);
        }

        void Handle_CompletedOnNameEntry(object sender, System.EventArgs e)
        {
            ageEntry.Focus();
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