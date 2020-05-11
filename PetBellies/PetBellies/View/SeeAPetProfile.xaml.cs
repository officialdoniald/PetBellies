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

        private List<User> followerList = new List<User>();

        public SeeAPetProfile(int petid)
        {
            this.petid = petid;

            InitializeComponent();

            InitializeThePetPictures();
        }

        private async void InitializeThePetPictures()
        {
            await Task.Run(async () =>
            {
                thisPet = GlobalVariables.petProfileFragmentViewModel.GetPetFromDBByID(petid);

                currentWidth = Application.Current.MainPage.Width;

                optimalWidth = currentWidth / 3;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Title = thisPet.Name;
                    ageLabel.Text = new Segédfüggvények().HowOld(thisPet.Age).ToString();
                    kindLabel.Text = thisPet.PetType;

                    profilePictureImage.HeightRequest = optimalWidth;
                    profilePictureImage.WidthRequest = optimalWidth;

                    profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(thisPet.Profilepicture));
                });

                if (!GlobalVariables.seeAnOwnerProfileViewModel.IsItABlockedUser(GlobalVariables.ActualUser.id, thisPet.Uploader))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        NavigationPage.SetHasNavigationBar(this, true);

                        detailGrid.IsVisible = true;
                        blockedLabel.IsVisible = false;
                    });

                    HaveIAlreadyFollow = GlobalVariables.petProfileFragmentViewModel.HaveIAlreadyFollow(GlobalVariables.ActualUsersEmail, petid);

                    thisPet = GlobalVariables.petProfileFragmentViewModel.GetPetFromDBByID(petid);

                    currentWidth = Application.Current.MainPage.Width;

                    optimalWidth = currentWidth / 3;

                    if (HaveIAlreadyFollow) followOrNot = GlobalVariables.petProfileFragmentViewModel.unfollowText;
                    else followOrNot = GlobalVariables.petProfileFragmentViewModel.followText;

                    await GetFollowers();

                    await GetPetsPictures();
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

        private async Task GetPetsPictures()
        {
            await Task.Run(() =>
            {
                int left = 0;
                int top = 0;

                int i = 1;

                var list = GlobalVariables.databaseConnection.GetPetpicturesIDSByPetID(thisPet.id);

                Dictionary<int, int[]> keyValuePairs = new Dictionary<int, int[]>();

                foreach (var item in list)
                {
                    keyValuePairs.Add(item, new int[2] { top, left });

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
                }

                foreach (var item in list)
                {
                    var petpicture = GlobalVariables.databaseConnection.GetPetPictureByID(item);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Image image = new Image();

                        image.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(petpicture.PictureURL));

                        image.HeightRequest = optimalWidth;

                        image.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 1,
                            TappedCallback = delegate
                            {
                                OnPictureClicked(petpicture);
                            }
                        });

                        image.Aspect = Aspect.AspectFill;

                        pictureListGrid.Children.Add(image, keyValuePairs[item][0], keyValuePairs[item][1]);
                    });
                }
            });
        }

        private async Task GetFollowers()
        {
            await Task.Run(() =>
            {
                followerList = GlobalVariables.followersViewModel.GetUserList(this.petid);

                Device.BeginInvokeOnMainThread(() =>
                {
                    followersLabel.Text = followerList.Count.ToString();
                    followersLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        NumberOfTapsRequired = 1,
                        TappedCallback = (arg1, arg2) => Handle_Tapped(followersLabel, null)
                    });
                    followersTextLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        NumberOfTapsRequired = 1,
                        TappedCallback = (arg1, arg2) => Handle_Tapped(followersLabel, null)
                    });
                });
            });
        }

        public void OnPictureClicked(Petpictures petpictures)
        {
            if (!GlobalVariables.databaseConnection.GetPetpicturesExistByPetPicturesID(petpictures.id))
            {
                Navigation.PushAsync(new NoPictureFoundPage());
            }
            else
            {
                Navigation.PushAsync(new SeeAPicturePage(petpictures));
            }
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new FollowersPage(followerList));
        }

        private async void MoreToolbarItem_Activated(object sender, EventArgs e)
        {
            var reported = await DisplayActionSheet("More", "Cancel", null, followOrNot, "Go to owner profile");

            if (reported == followOrNot)
            {
                if (HaveIAlreadyFollow)
                {
                    string success = GlobalVariables.petProfileFragmentViewModel.UnFollow(GlobalVariables.ActualUsersEmail, petid);

                    HaveIAlreadyFollow = !HaveIAlreadyFollow;

                    if (!string.IsNullOrEmpty(success))
                    {
                        GlobalEvents.OnUnFollowUser_Event(this, null);

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
                        GlobalEvents.OnUnFollowUser_Event(this, null);

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