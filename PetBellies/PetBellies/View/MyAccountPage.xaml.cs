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
    public partial class MyAccountPage : ContentPage
    {
        private List<Following> followings = new List<Following>();

        public MyAccountPage()
        {
            InitializeComponent();

            GlobalEvents.OnProfileUpdated += GlobalEvents_OnProfileUpdated;
            GlobalEvents.OnProfilePictureUpdated += GlobalEvents_OnProfilePictureUpdated;
            GlobalEvents.OnPetAdded += GlobalEvents_OnPetAdded;
            GlobalEvents.OnPetDeleted += GlobalEvents_OnPetDeleted;

            Device.BeginInvokeOnMainThread(() =>
            {
                profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(GlobalVariables.ActualUser.ProfilePicture));

                Title = GlobalVariables.ActualUser.FirstName + " " + GlobalVariables.ActualUser.LastName;
            });

            GetPets();
        }

        private async void GlobalEvents_OnPetDeleted(object sender, object e)
        {
            await GetPets();
        }

        private async void GlobalEvents_OnPetAdded(object sender, object e)
        {
            await GetPets();
        }

        private void GlobalEvents_OnProfilePictureUpdated(object sender, object e)
        {
            profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(GlobalVariables.ActualUser.ProfilePicture));
        }

        private void GlobalEvents_OnProfileUpdated(object sender, object e)
        {
            Title = GlobalVariables.ActualUser.FirstName + " " + GlobalVariables.ActualUser.LastName;
        }

        protected override async void OnAppearing()
        {
            var currentWidth = Application.Current.MainPage.Width;

            var optimalWidth = currentWidth / 3;

            profilePictureImage.HeightRequest = optimalWidth;
            profilePictureImage.WidthRequest = optimalWidth;

            await GetFollowings();
        }

        private async Task GetPets()
        {
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    petListStackLayout.Children.Clear();

                });
                foreach (var item in GlobalVariables.Mypetlist)
                {
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
                        Source = ImageSource.FromStream(() => new System.IO.MemoryStream(item.ProfilePictureURL))
                    };

                    Label nameLabel = new Label()
                    {
                        Text = item.Name,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Color.Black
                    };
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        stackLayout.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 1,
                            TappedCallback = delegate { TapPet(item.petid); }
                        });

                        stackLayout.Children.Add(petProfilePictureImage);
                        stackLayout.Children.Add(nameLabel);
                        frame.Content = stackLayout;
                        petListStackLayout.Children.Add(frame);
                    });
                }
            });
        }

        private async Task GetFollowings()
        {
            await Task.Run(() =>
            {
                followings = GlobalVariables.myAccountPageViewModel.GetMyFollowing();

                Device.BeginInvokeOnMainThread(() =>
                {
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

        private async void TapPet(int id)
        {
            await Task.Run(()=> {
                var searchResultPage = new SeeMyPetProfile(id);

                Device.BeginInvokeOnMainThread(()=> {
                    Navigation.PushAsync(searchResultPage);
                });
            });
        }

        //Following gomb
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FollowersPage(followings));
        }

        private async void MoreToolbarItem_Activated(object sender, EventArgs e)
        {
            var reported = await DisplayActionSheet("More", "Cancel", null, "Add pet", "Settings");

            if (reported == "Add pet")
            {
                await Navigation.PushAsync(new AddPetPage());
            }
            else if (reported == "Settings")
            {
                await Navigation.PushAsync(new OtherPage());
            }
        }
    }
}