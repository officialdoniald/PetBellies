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

        private string followOrNot = string.Empty;

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
                    followersLabel.Text = GlobalVariables.followersViewModel.GetUserList(this.petid).Count.ToString();

                    currentWidth = Application.Current.MainPage.Width;

                    optimalWidth = currentWidth / 3;

                    petnameLabel.Text = thisPet.Name;
                    ageLabel.Text = new Segédfüggvények().HowOld(thisPet.Age).ToString();
                    kindLabel.Text = thisPet.PetType;

                    profilePictureImage.HeightRequest = optimalWidth;
                    profilePictureImage.WidthRequest = optimalWidth;

                    if (thisPet.Profilepicture != null)
                        profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(thisPet.Profilepicture));
                    else profilePictureImage.Source = "account.png";
                });

                if (!GlobalVariables.seeAnOwnerProfileViewModel.IsItABlockedUser(thisPet.Uploader))
                {
                    Device.BeginInvokeOnMainThread(()=>
                    {
                        NavigationPage.SetHasNavigationBar(this, true);
                        detailGrid.IsVisible = true;
                        blockedLabel.IsVisible = false;
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
                        if (HaveIAlreadyFollow) followOrNot = GlobalVariables.petProfileFragmentViewModel.unfollowText;
                        else followOrNot = GlobalVariables.petProfileFragmentViewModel.followText;
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        detailGrid.IsVisible = false;
                        blockedLabel.IsVisible = true;
                        blockedLabel.Text = English.BlockedPetUser();
                        NavigationPage.SetHasNavigationBar(this, false);
                    });
                }
            });
        }

        public void OnPictureClicked(Petpictures petpictures)
        {
            Navigation.PushAsync(new SeeAPicturePage(petpictures));
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new FollowersPage(thisPet.id));
        }

        private async void MoreToolbarItem_Activated(object sender, EventArgs e)
        {
            var reported = await DisplayActionSheet("More", "Cancel", followOrNot, "Go to owner profile");

            if (reported == followOrNot)
            {
                if (HaveIAlreadyFollow)
                {
                    string success = GlobalVariables.petProfileFragmentViewModel.UnFollow(GlobalVariables.ActualUsersEmail, petid);

                    HaveIAlreadyFollow = !HaveIAlreadyFollow;

                    if (!string.IsNullOrEmpty(success))
                    {
                        await DisplayAlert(English.Failed(), success, English.OK());
                    }
                    else
                    {
                        followOrNot = GlobalVariables.petProfileFragmentViewModel.followText;
                    }
                }
                else
                {
                    string success = GlobalVariables.petProfileFragmentViewModel.FollowAPet(GlobalVariables.ActualUsersEmail, petid);

                    HaveIAlreadyFollow = !HaveIAlreadyFollow;

                    if (!string.IsNullOrEmpty(success))
                    {
                        await DisplayAlert(English.Failed(), success, English.OK());
                    }
                    else
                    {
                        followOrNot = GlobalVariables.petProfileFragmentViewModel.unfollowText;
                    }
                }
            }
            else if (reported == "Go to owner profile")
            {
                await Navigation.PushAsync(new SeeAnOwnerPage(thisPet.Uploader));
            }
        }
    }
}