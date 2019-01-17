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
	public partial class UploadPhotoPage : ContentPage
	{
        private int selectedPetId = -1;
        private double currentWidth;
        private bool addedPhoto = false;
        private Stream f;

        public UploadPhotoPage()
        {
            Initialize();

            currentWidth = Application.Current.MainPage.Width;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            GlobalVariables.CanIGoBackWithTheBackButton = true;
        }

        protected override void OnAppearing()
        {
            GlobalVariables.CanIGoBackWithTheBackButton = false;

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

                petPicker.Title = "Select a pet";

                petPicker.IsEnabled = true;

                petPicker.ItemsSource = GlobalVariables.MyPetsString;
            }
            else
            {
                petPicker.IsEnabled = false;

                petPicker.Title = "Add a pet before upload a photo";
            }

            pictureImage.IsVisible = false;
        }

        private async Task galleryButton_ClickedAsync(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert(English.Failed(), English.NoPicking(), English.OK());
                return;
            }

            pictureImage.IsVisible = true;

            var file = await CrossMedia.Current.PickPhotoAsync();
            
            if (file == null) return;

            addedPhoto = true;

            f = file.GetStream();

            GlobalVariables.SourceSelectedImageFromGallery = file.Path;
            GlobalVariables.SelectedImageFromGallery = f;

            pictureImage.Source = ImageSource.FromStream(() => f);
        }

        private void addPhotoButton_ClickedAsync(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    uploadActivity.IsRunning = true;
                    addPhotoButton.IsEnabled = false;
                    galleryButton.IsEnabled = false;
                });

                string success = GlobalVariables.uploadPhotoFragmentViewModel.UploadPictureAsync(addedPhoto, f, selectedPetId, hashtagsEntry.Text);

                if (!string.IsNullOrEmpty(success))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert(English.Failed(), success, English.OK());
                    });
                }
                else
                {
                    GlobalVariables.AddedPhoto = true;
                    addedPhoto = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopToRootAsync();
                        Navigation.PushAsync(new SeeMyPetProfile(selectedPetId));
                    });
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    galleryButton.IsEnabled = true;
                    addPhotoButton.IsEnabled = true;
                    uploadActivity.IsRunning = false;
                });
            });
        }

        private void petPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPetId = GlobalVariables.Mypetlist[petPicker.SelectedIndex].petid;
        }

        private void Handle_Completed(object sender, System.EventArgs e)
        {
            addPhotoButton_ClickedAsync(this, new EventArgs());
        }
    }
}