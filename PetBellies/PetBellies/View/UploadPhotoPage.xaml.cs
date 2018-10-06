using PetBellies.BLL.Helper;
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
        private int needToRotate = 0;
        private int rot = 0;
        double currentWidth;

        bool asdx = true;
        private double height = 0;
        private double width = 0;

        public UploadPhotoPage()
        {
            Initialize();

            currentWidth = Application.Current.MainPage.Width;
        }

        protected override void OnAppearing()
        {
            if (GlobalVariables.AddedPet || GlobalVariables.AddedPhoto)
            {
                Initialize();
                rot = 0;
            }
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

            asdx = true;
        }

        private async Task addPhotoButton_ClickedAsync(object sender, EventArgs e)
        {
            uploadActivity.IsRunning = true;
            addPhotoButton.IsEnabled = false;
            galleryButton.IsEnabled = false;

            string success = await GlobalVariables.uploadPhotoFragmentViewModel.UploadPictureAsync(GlobalVariables.pathf, GlobalVariables.f, selectedPetId, hashtagsEntry.Text);

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

        async Task Handle_Completed(object sender, System.EventArgs e)
        {
            await addPhotoButton_ClickedAsync(this, new EventArgs());
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            setRotValue(90);
        }

        void Handle_Clicked2(object sender, System.EventArgs e)
        {
            setRotValue(-90);
        }

        void Handle_Clicked3(object sender, System.EventArgs e)
        {
            setRotValue(0);
        }

        void needToRotateorNot(int rotation)
        {
            if (rotation != 0)
            {
                needToRotate = 1;
            }
            else
            {
                needToRotate = 0;
            }
        }

        void rotateAndHeighAndWidthSet()
        {
            if (Math.Abs(rot) == 360 || Math.Abs(rot) == 0 || Math.Abs(rot) == 180)
            {
                pictureImage.HeightRequest = height;
                pictureImage.WidthRequest = width;
            }
            else if (Math.Abs(rot) == 90 || Math.Abs(rot) == 270)
            {
                pictureImage.HeightRequest = width;
            }
        }

        void setRotValue(int howmany)
        {
            if (asdx)
            {
                height = pictureImage.Height;
                width = pictureImage.Width;

                pictureImage.HeightRequest = height;
                pictureImage.WidthRequest = width;

                asdx = false;
            }

            if (rot + howmany >= 360 || rot + howmany < -360 || howmany == 0 || rot + howmany == 0)
            {
                rot = 0;
            }
            else if (rot + howmany == 90)
            {
                rot = 90;
            }
            else if (rot + howmany == 180)
            {
                rot = 180;
            }
            else if (rot + howmany == 270)
            {
                rot = 270;
            }
            else if (rot + howmany == -90)
            {
                rot = -90;
            }
            else if (rot + howmany == -180)
            {
                rot = -180;
            }
            else if (rot + howmany == -270)
            {
                rot = -270;
            }
            else if (rot + howmany == -360)
            {
                rot = -360;
            }

            needToRotateorNot(rot);
            pictureImage.Rotation = rot;
            rotateAndHeighAndWidthSet();
        }
    }
}