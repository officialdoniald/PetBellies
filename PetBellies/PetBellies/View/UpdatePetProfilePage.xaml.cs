using PetBellies.BLL.Helper;
using PetBellies.Model;
using Plugin.Media;
using System;
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

        private Pet thisPet = new Pet();

        public UpdatePetProfilePage(int petid)
        {
            this.petid = petid;

            InitializeComponent();

            thisPet = GlobalVariables.ConvertMyPetListToPet(GlobalVariables.Mypetlist.Where(i => i.petid == petid).FirstOrDefault());

            nameEntry.Placeholder = thisPet.Name;
            ageEntry.Placeholder = thisPet.Age.ToString();
            typeEntry.Placeholder = thisPet.PetType;

            if (thisPet.ProfilePictureURL != null)
            {
                //profilePictureImage.Source = ImageSource.FromUri(new Uri(thisPet.ProfilePictureURL));

                profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(thisPet.ProfilePictureURL));
            }

            if (thisPet.HaveAnOwner == 0) shelterpetSwitch.IsToggled = true;
            else shelterpetSwitch.IsToggled = false;
        }

        private async void deletePetButton_ClickedAsync(object sender, EventArgs e)
        {
            string success = GlobalVariables.updatePetFragmentViewModel.DeletePet(petid);

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(), success, English.OK());
            }
            else
            {
                await Navigation.PopToRootAsync();
            }
        }

        private async void changePetButton_ClickedAsync(object sender, EventArgs e)
        {
            bool isChecked = shelterpetSwitch.IsToggled;

            int isCheckedToInt = 1;

            if (isChecked) isCheckedToInt = 0;

            int age;

            if (String.IsNullOrEmpty(ageEntry.Text))
            {
                age = -1;
            }
            else
            {
                age = Convert.ToInt32(ageEntry.Text);
            }

            Pet pet = new Pet();

            pet = thisPet;
            pet.HaveAnOwner = isCheckedToInt;

            if (!String.IsNullOrEmpty(nameEntry.Text))
            {
                pet.Name = nameEntry.Text;
            }
            if (age != -1)
            {
                pet.Age = age;
            }
            if (!String.IsNullOrEmpty(typeEntry.Text))
            {
                pet.PetType = typeEntry.Text;
            }

            string success = GlobalVariables.updatePetFragmentViewModel.UpdatePetProfile(pet);

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(), success, English.OK());
            }
            else
            {
                await Navigation.PopAsync();
            }
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

        private async void changeProfilePictureButton_Clicked(object sender, EventArgs e)
        {
            string success = await GlobalVariables.updatePetFragmentViewModel.UpdatePetProfilePictureAsync(thisPet, GlobalVariables.f, GlobalVariables.pathf);

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(), success, English.OK());
            }
            else
            {
                await Navigation.PopAsync();
            }
        }
    }
}