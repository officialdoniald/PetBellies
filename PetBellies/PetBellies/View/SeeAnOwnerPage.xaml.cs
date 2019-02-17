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

        private async Task Initialize()
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

                if (!GlobalVariables.seeAnOwnerProfileViewModel.IsItABlockedUser(userID))//ha nem blokkolt
                {
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
                        followingAltLabel.IsVisible = true;
                        petListStackLayout.IsVisible = true;

                        followings = GlobalVariables.seeAnOwnerProfileViewModel.GetUsersFollowing(userID);
                        followingLabel.Text = followings.Count.ToString();
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
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        blockedLabel.Text = English.BlockedUser();
                        blockedLabel.IsVisible = true;
                        //petsLabel.IsVisible = false;
                        followingLabel.IsVisible = false;
                        followingAltLabel.IsVisible = false;
                        petListStackLayout.IsVisible = false;
                    });
                }
            });
        }

        private async void ReportToolbarItem_Clicked(object sender, EventArgs e)
        {
            var reported = await DisplayActionSheet("More", "Cancel", "Report", "Block");

            if (reported == "Report")
            {
                var success = GlobalVariables.seeAnOwnerProfileViewModel.ReportUser(user);

                if (success)
                {
                    try
                    {
                        //Aki reportolt
                        string url = string.Format("http://petbellies.com/php/petbelliesreppic.php?email={0}&nev={1}&host={2}", user.Email, user.FirstName, 0);
                        Uri uri = new Uri(url);
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                        request.Method = "GET";
                        WebResponse res = await request.GetResponseAsync();

                        //Akinek a képe van
                        string url1 = string.Format("http://petbellies.com/php/petbelliesreppic.php?email={0}&nev={1}&host={2}", GlobalVariables.ActualUsersEmail, GlobalVariables.ActualUser.FirstName, 1);
                        Uri uri1 = new Uri(url);
                        HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(uri1);
                        request.Method = "GET";
                        WebResponse res1 = await request.GetResponseAsync();
                    }
                    catch (Exception) { }

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

        private void blockUserButton_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}