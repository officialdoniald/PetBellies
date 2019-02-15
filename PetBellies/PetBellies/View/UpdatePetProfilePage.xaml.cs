using PetBellies.BLL.Helper;
using PetBellies.Model;
using Plugin.Media;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdatePetProfilePage : ContentPage
	{
        private int petid = -1;
        private bool addedPhoto = false;
        private Pet thisPet = new Pet();
        private Stream f;

        public UpdatePetProfilePage(int petid)
        {
            InitializeComponent();

            this.petid = petid;

            ageDatePicker.MaximumDate = DateTime.Now;

            thisPet = GlobalVariables.ConvertMyPetListToPet(GlobalVariables.Mypetlist.Where(i => i.petid == petid).FirstOrDefault());

            Device.BeginInvokeOnMainThread(() => {
                nameEntry.Placeholder = thisPet.Name;
                ageDatePicker.Date = thisPet.Age;
                typeEntry.Placeholder = thisPet.PetType;

                if (thisPet.Profilepicture != null)
                    profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(thisPet.Profilepicture));

                if (thisPet.HaveAnOwner == 0) shelterpetSwitch.IsToggled = true;
                else shelterpetSwitch.IsToggled = false;
            });
        }

        private void deletePetButton_ClickedAsync(object sender, EventArgs e)
        {
            Task.Run(()=> {
                DisableOrEnableButtons(false);

                string success = GlobalVariables.updatePetFragmentViewModel.DeletePet(petid);

                if (!string.IsNullOrEmpty(success))
                    Device.BeginInvokeOnMainThread(()=> {
                        DisplayAlert(English.Failed(), success, English.OK());
                    });
                else Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopToRootAsync();
                });

                DisableOrEnableButtons(true);
            });
        }

        private void changePetButton_ClickedAsync(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                DisableOrEnableButtons(false);

                bool isChecked = shelterpetSwitch.IsToggled;

                int isCheckedToInt = 1;

                if (isChecked) isCheckedToInt = 0;
                
                Pet pet = new Pet();

                pet = thisPet;
                pet.HaveAnOwner = isCheckedToInt;

                if (!string.IsNullOrEmpty(nameEntry.Text))
                {
                    pet.Name = nameEntry.Text;
                }
                
                pet.Age = ageDatePicker.Date;
                
                if (!string.IsNullOrEmpty(typeEntry.Text))
                {
                    pet.PetType = typeEntry.Text;
                }

                string success = GlobalVariables.updatePetFragmentViewModel.UpdatePetProfile(pet);

                if (!string.IsNullOrEmpty(success))
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

        private void changeProfilePictureButton_Clicked(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                DisableOrEnableButtons(false);

                string success = GlobalVariables.updatePetFragmentViewModel.UpdatePetProfilePictureAsync(addedPhoto, thisPet, f);

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
                updatePetProfileActivator.IsRunning = !enable;
                galleryButton.IsEnabled = enable;
                changeProfilePictureButton.IsEnabled = enable;
                deletePetButton.IsEnabled = enable;
                changePetButton.IsEnabled = enable;
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
    }
}