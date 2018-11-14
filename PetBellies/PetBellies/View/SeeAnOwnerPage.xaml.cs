using ImageCircle.Forms.Plugin.Abstractions;
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
	public partial class SeeAnOwnerPage : ContentPage
	{
        private int userID = -1;

        private List<Pet> petList = new List<Pet>();

        private List<Following> followings = new List<Following>();

        double currentWidth = 0;

        double optimalWidth = 0;

        public SeeAnOwnerPage(int userid)
        {
            userID = userid;

            InitializeComponent();

            Initialize();
        }

        private async Task Initialize()
        {
            await Task.Run(() =>
            {
                User user = GlobalVariables.databaseConnection.GetUserByID(userID);

                if (user.ProfilePictureURL != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        currentWidth = Application.Current.MainPage.Width;

                        optimalWidth = currentWidth / 3;

                        profilePictureImage.HeightRequest = optimalWidth;
                        profilePictureImage.WidthRequest = optimalWidth;

                        profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(user.ProfilePictureURL));
                    });
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    currentWidth = Application.Current.MainPage.Width;

                    optimalWidth = currentWidth / 5;

                    userNameLabel.Text = user.FirstName + " " + user.LastName;
                });

                if (!GlobalVariables.seeAnOwnerProfileViewModel.IsItABlockedUser(userID))//ha nem blokkolt
                {
                    petList = GlobalVariables.seeAnOwnerProfileViewModel.GetPet(user.id);

                    foreach (var item in petList)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            blockedLabel.IsVisible = false;
                            petsLabel.IsVisible = true;
                            detailGrid.IsVisible = true;

                            followings = GlobalVariables.seeAnOwnerProfileViewModel.GetUsersFollowing(userID);
                            followingLabel.Text = followings.Count.ToString();

                            StackLayout oneGrid = new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical
                            };

                            CircleImage petProfilePictureImage = new CircleImage
                            {
                                HeightRequest = optimalWidth,
                                WidthRequest = optimalWidth,
                                Aspect = Aspect.AspectFill,
                                HorizontalOptions = LayoutOptions.Center,
                                Source = ImageSource.FromStream(() => new System.IO.MemoryStream(item.ProfilePictureURL)),
                            };

                            Label petNameLabel = new Label()
                            {
                                Text = item.Name,
                                Style = GlobalVariables.NormalLabel
                            };

                            var goToPetProfileTapped = new TapGestureRecognizer();
                            goToPetProfileTapped.Tapped += (s, e) =>
                            {
                                var searchResultPage = new SeeAPetProfile(item.id);

                                Navigation.PushAsync(searchResultPage);
                            };

                            petProfilePictureImage.GestureRecognizers.Add(goToPetProfileTapped);
                            petNameLabel.GestureRecognizers.Add(goToPetProfileTapped);

                            oneGrid.Children.Add(petProfilePictureImage);
                            oneGrid.Children.Add(petNameLabel);

                            petsStackLayout.Children.Add(oneGrid);
                        });
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        blockedLabel.Text = English.BlockedUser();
                        blockedLabel.IsVisible = true;
                        petsLabel.IsVisible = false;
                        detailGrid.IsVisible = false;
                    });
                }
            });
        }

        //Following gomb
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FollowersPage(followings));
        }

        private void blockUserButton_Clicked(object sender, EventArgs e)
        {
            var success = GlobalVariables.blockedPeopleViewModel.InsertBlockedPeople(
            new Model.BlockedPeople()
            {
                UserID = GlobalVariables.ActualUser.id,
                BlockedUserID = userID
            });

            if (!string.IsNullOrEmpty(success))
                DisplayAlert(English.Failed(), success, English.OK());
            else Navigation.PopToRootAsync();
        }
    }
}