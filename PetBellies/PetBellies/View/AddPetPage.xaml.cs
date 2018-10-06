using PetBellies.BLL.Helper;
using PetBellies.Model;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddPetPage : ContentPage
	{
        public AddPetPage()
        {
            InitializeComponent();
        }

        private async Task addPetButton_ClickedAsync(object sender, EventArgs e)
        {
            uploadActivity.IsRunning = true;
            addPetButton.IsEnabled = false;
            galleryButton.IsEnabled = false;

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

            string success = await GlobalVariables.addpetFragmentViewModel.AddPetAsync(GlobalVariables.pathf, GlobalVariables.f, pet);

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(), success, English.OK());
            }
            else
            {
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

            GlobalVariables.mediaFile = file;

            if (file == null) return;

            GlobalVariables.f = file.GetStream();
            GlobalVariables.pathf = file.Path;

            profilePictureImage.Source = ImageSource.FromStream(() => file.GetStream());
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