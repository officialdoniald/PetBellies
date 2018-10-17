using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SeeAPetProfile : ContentPage
	{
        private int petid = -1;

        private double currentWidth = 0;
        private double optimalWidth = 0;

        private bool HaveIAlreadyFollow = false;

        private Pet thisPet = new Pet();

        private List<Petpictures> petPictureListfromDB = new List<Petpictures>();

        private Petpictures petpictures = new Petpictures();

        public SeeAPetProfile(int petid)
        {
            this.petid = petid;

            InitializeComponent();

            InitializeThePetPictures();
        }

        private async Task InitializeThePetPictures()
        {
            await Task.Run(() =>
            {
                petPictureListfromDB = GlobalVariables.petProfileFragmentViewModel.GetPetPictureURL(petid);

                thisPet = GlobalVariables.petProfileFragmentViewModel.GetPetFromDBByID(petid);

                Device.BeginInvokeOnMainThread(() =>
                {
                    followersLabel.Text = GlobalVariables.followersViewModel.GetUserList(this.petid).Count + " followers";

                    currentWidth = Application.Current.MainPage.Width;

                    optimalWidth = currentWidth / 3;

                    petnameLabel.Text = thisPet.Name;

                    profilePictureImage.HeightRequest = optimalWidth;
                    profilePictureImage.WidthRequest = optimalWidth;

                    if (thisPet.ProfilePictureURL != null)
                    {
                        profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(thisPet.ProfilePictureURL));
                    } else profilePictureImage.Source = "";
                });

                HaveIAlreadyFollow = GlobalVariables.petProfileFragmentViewModel.HaveIAlreadyFollow(GlobalVariables.ActualUsersEmail, petid);

                int left = 0;
                int top = 0;

                int i = 1;

                foreach (var item in petPictureListfromDB)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Image image = new Image();

                        //image.Source = ImageSource.FromUri(new Uri(item.PictureURL));

                        image.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(item.PictureURL));

                        image.HeightRequest = optimalWidth;

                        image.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 1,
                            TappedCallback = delegate
                            {
                                OnPictureClicked(item);
                            }
                        });

                        image.Aspect = Aspect.AspectFill;

                        pictureListGrid.Children.Add(image, top, left);

                        if (i == 3)
                        {
                            left++;
                            i = 1;
                            top = 0;
                        }
                        else
                        {
                            i++;
                            top++;
                        }
                    });
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    if (HaveIAlreadyFollow) followButton.Text = GlobalVariables.petProfileFragmentViewModel.unfollowText;
                    else followButton.Text = GlobalVariables.petProfileFragmentViewModel.followText;
                });
            });
        }

        private async System.Threading.Tasks.Task followButton_ClickedAsync(object sender, EventArgs e)
        {
            if (HaveIAlreadyFollow)
            {
                string success = GlobalVariables.petProfileFragmentViewModel.UnFollow(GlobalVariables.ActualUsersEmail, petid);

                HaveIAlreadyFollow = !HaveIAlreadyFollow;

                if (!String.IsNullOrEmpty(success))
                {
                    await DisplayAlert(English.Failed(), success, English.OK());
                }
                else
                {
                    followButton.Text = GlobalVariables.petProfileFragmentViewModel.followText;
                }
            }
            else
            {
                string success = GlobalVariables.petProfileFragmentViewModel.FollowAPet(GlobalVariables.ActualUsersEmail, petid);

                HaveIAlreadyFollow = !HaveIAlreadyFollow;

                if (!String.IsNullOrEmpty(success))
                {
                    await DisplayAlert(English.Failed(), success, English.OK());
                }
                else
                {
                    followButton.Text = GlobalVariables.petProfileFragmentViewModel.unfollowText;
                }
            }
        }

        private void goToOwnerPageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeeAnOwnerPage(thisPet.Uploader));
        }

        public void OnPictureClicked(Petpictures petpictures)
        {
            Navigation.PushAsync(new SeeAPicturePage(petpictures));
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new FollowersPage(thisPet.id));
        }
    }
}