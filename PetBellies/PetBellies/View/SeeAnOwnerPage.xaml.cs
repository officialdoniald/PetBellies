using ImageCircle.Forms.Plugin.Abstractions;
using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SeeAnOwnerPage : ContentPage, INotifyPropertyChanged
    {
        private int userID = -1;

        private List<Pet> petList = new List<Pet>();

        private User user;

        private List<Following> followings = new List<Following>();

        double currentWidth = 0;

        double optimalWidth = 0;

        private int _position;
        public int Position { get { return _position; } set { _position = value; OnPropertyChanged(); } }

        public SeeAnOwnerPage(int userid)
        {
            userID = userid;

            InitializeComponent();

            Initialize();
        }

        private async void Initialize()
        {
            await Task.Run(() =>
            {
                user = GlobalVariables.databaseConnection.GetUserByID(userID);

                if (user.ProfilePicture != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        currentWidth = Application.Current.MainPage.Width;

                        optimalWidth = currentWidth / 3;

                        profilePictureImage.HeightRequest = optimalWidth;
                        profilePictureImage.WidthRequest = optimalWidth;

                        profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(user.ProfilePicture));
                    });
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    currentWidth = Application.Current.MainPage.Width;

                    optimalWidth = currentWidth / 5;

                    Title = user.FirstName + " " + user.LastName;
                });

                if (!GlobalVariables.seeAnOwnerProfileViewModel.IsItABlockedUser(GlobalVariables.ActualUser.id, userID))//ha nem blokkolt
                {
                    Task.Run(()=> {
                        petList = GlobalVariables.seeAnOwnerProfileViewModel.GetPet(user.id);

                        List<ImageAndText> imageAndTexts = new List<ImageAndText>();

                        ToolbarItem MoreToolbarItem = new ToolbarItem()
                        {
                            Icon = "more.png",
                            Priority = 0
                        };

                        MoreToolbarItem.Clicked += ReportToolbarItem_Clicked;

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            NavigationPage.SetHasNavigationBar(this, true);

                            ToolbarItems.Add(MoreToolbarItem);

                            blockedLabel.IsVisible = false;
                            //petsLabel.IsVisible = true;
                            followingLabel.IsVisible = true;
                            followersTextLabel.IsVisible = true;
                            petListStackLayout.IsVisible = true;

                            GetFollowings();
                        });

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            foreach (var item in petList)
                            {
                                imageAndTexts.Add(new ImageAndText()
                                {
                                    Pet = item,
                                    Name = item.Name,
                                    ProfilePictureURL = ImageSource.FromStream(() => new System.IO.MemoryStream(item.Profilepicture))
                                });

                                Frame frame = new Frame()
                                {
                                    //BorderColor = Color.LightGray,
                                    //Padding = 10,
                                    //BackgroundColor = Color.FromHex("#F5F6F8")
                                    BackgroundColor = Color.White
                                };

                                StackLayout stackLayout = new StackLayout()
                                {
                                    Orientation = StackOrientation.Vertical
                                };

                                CircleImage petProfilePictureImage = new CircleImage
                                {
                                    HeightRequest = 55,
                                    WidthRequest = 55,
                                    Aspect = Aspect.AspectFill,
                                    HorizontalOptions = LayoutOptions.Center,
                                    Source = ImageSource.FromStream(() => new System.IO.MemoryStream(item.Profilepicture))
                                };

                                Label nameLabel = new Label()
                                {
                                    Text = item.Name,
                                    HorizontalOptions = LayoutOptions.Center,
                                    TextColor = Color.Black
                                };

                                stackLayout.GestureRecognizers.Add(new TapGestureRecognizer()
                                {
                                    NumberOfTapsRequired = 1,
                                    TappedCallback = delegate { TapPet(item.id); }
                                });

                                stackLayout.Children.Add(petProfilePictureImage);
                                stackLayout.Children.Add(nameLabel);
                                frame.Content = stackLayout;
                                petListStackLayout.Children.Add(frame);
                            }
                        });
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        blockedLabel.Text = English.BlockedUser();
                        blockedLabel.IsVisible = true;
                        //petsLabel.IsVisible = false;
                        followingLabel.IsVisible = false;
                        followersTextLabel.IsVisible = false;
                        petListStackLayout.IsVisible = false;
                    });
                }
            });
        }

        private void GetFollowings()
        {
            Task.Run(()=> {
                followings = GlobalVariables.seeAnOwnerProfileViewModel.GetUsersFollowing(userID);

                Device.BeginInvokeOnMainThread(() => {
                    followingLabel.Text = followings.Count.ToString();
                    followingLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        NumberOfTapsRequired = 1,
                        TappedCallback = (arg1, arg2) => TapGestureRecognizer_Tapped(followingLabel, null)
                    });
                    followersTextLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        NumberOfTapsRequired = 1,
                        TappedCallback = (arg1, arg2) => TapGestureRecognizer_Tapped(followersTextLabel, null)
                    });
                });
            });
        }

        private async void ReportToolbarItem_Clicked(object sender, EventArgs e)
        {
            var reported = await DisplayActionSheet("More", "Cancel", null, "Report", "Block");

            if (reported == "Report")
            {
                var success = GlobalVariables.seeAnOwnerProfileViewModel.ReportUser(user);

                if (success)
                {
                    await DisplayAlert("Success", "Thanks..", "OK");
                }
                else
                {
                    await DisplayAlert("Failed", "Something went wrong", "OK");
                }
            }
            else if (reported == "Block")
            {
                var success = GlobalVariables.blockedPeopleViewModel.InsertBlockedPeople(
                new Model.BlockedPeople()
                {
                    UserID = GlobalVariables.ActualUser.id,
                    BlockedUserID = userID
                });

                if (!string.IsNullOrEmpty(success))
                    await DisplayAlert(English.Failed(), success, English.OK());
                else
                {
                    await DisplayAlert("Success", "Successful blocked!", "OK");
                    await Navigation.PopToRootAsync();
                }
            }
        }

        private void TapPet(int id)
        {
            var searchResultPage = new SeeAPetProfile(id);

            Navigation.PushAsync(searchResultPage);
        }

        //Following gomb
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FollowersPage(followings));
        }
    }
}