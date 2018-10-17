using PetBellies.BLL.Helper;
using PetBellies.DAL;
using Plugin.Media;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UploadPhotoPage : ContentPage
	{
        private int selectedPetId = -1;
        private double currentWidth;

        public UploadPhotoPage()
        {
            Initialize();

            currentWidth = Application.Current.MainPage.Width;
        }

        protected override void OnAppearing()
        {
            //DatabaseConnections databaseConnection = new DatabaseConnections();

            //pictureImage.Source = ImageSource.FromStream(() => databaseConnection.PictureFromDB());
            //pictureImage.Aspect = Aspect.Fill;
            if (GlobalVariables.AddedPet || GlobalVariables.AddedPhoto)
                Initialize();
        }

        private void Initialize()
        {
            InitializeComponent();

            if (GlobalVariables.Mypetlist.Count != 0)
            {
                selectedPetId = GlobalVariables.Mypetlist[0].petid;

                GlobalVariables.AddedPet = false;
                GlobalVariables.AddedPhoto = false;
            }

            petPicker.ItemsSource = GlobalVariables.MyPetsString;
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

            pictureImage.Source = ImageSource.FromStream(() => file.GetStream());
        }
        
        private async Task addPhotoButton_ClickedAsync(object sender, EventArgs e)
        {
            uploadActivity.IsRunning = true;
            addPhotoButton.IsEnabled = false;
            galleryButton.IsEnabled = false;
            
            string success = await GlobalVariables.uploadPhotoFragmentViewModel.UploadPictureAsync(GlobalVariables.pathf, GlobalVariables.f, selectedPetId, hashtagsEntry.Text);
            //DatabaseConnections databaseConnection = new DatabaseConnections();
            //databaseConnection.InsertToImageTable(GlobalVariables.f);
            
            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(), success, English.OK());
            }
            else
            {
                GlobalVariables.AddedPhoto = true;

                await Navigation.PopToRootAsync();
                await Navigation.PushAsync(new SeeMyPetProfile(selectedPetId));
            }

            galleryButton.IsEnabled = true;
            addPhotoButton.IsEnabled = true;
            uploadActivity.IsRunning = false;
        }

        private void petPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPetId = GlobalVariables.Mypetlist[petPicker.SelectedIndex].petid;
        }

        private async Task Handle_Completed(object sender, System.EventArgs e)
        {
            await addPhotoButton_ClickedAsync(this, new EventArgs());
        }
    }
}