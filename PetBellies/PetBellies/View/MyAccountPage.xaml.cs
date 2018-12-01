using ImageCircle.Forms.Plugin.Abstractions;
using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyAccountPage : ContentPage
	{
        private List<ListViewWithPictureAndSomeText> listViewWithPictureAndSomeText = new List<ListViewWithPictureAndSomeText>();

        private List<Following> followings = new List<Following>();

        public MyAccountPage()
        {
            InitializeComponent();

            Device.BeginInvokeOnMainThread(() => {
                Title = GlobalVariables.ActualUser.FirstName + " " + GlobalVariables.ActualUser.LastName;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            GlobalVariables.CanIGoBackWithTheBackButton = true;
        }

        protected override void OnAppearing()
        {
            //Ha leszkedved szenvedj vele, mert bugos lesz, ha más pageről hívod be.
            GlobalVariables.CanIGoBackWithTheBackButton = false;
            
            var currentWidth = Application.Current.MainPage.Width;

            var optimalWidth = currentWidth / 3;

            profilePictureImage.HeightRequest = optimalWidth;
            profilePictureImage.WidthRequest = optimalWidth;

            if (GlobalVariables.ActualUser.ProfilePictureURL != null)
                profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(GlobalVariables.ActualUser.ProfilePictureURL));
            else profilePictureImage.Source = "account.png";

            Title = GlobalVariables.ActualUser.FirstName + " " + GlobalVariables.ActualUser.LastName;

            followings = GlobalVariables.myAccountPageViewModel.GetMyFollowing();

            followingLabel.Text = followings.Count.ToString();

            listViewWithPictureAndSomeText = new List<ListViewWithPictureAndSomeText>();

            petListStackLayout.Children.Clear();

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

                stackLayout.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1,
                    TappedCallback = delegate { TapPet(item.petid); }
                });

                stackLayout.Children.Add(petProfilePictureImage);
                stackLayout.Children.Add(nameLabel);
                frame.Content = stackLayout;
                petListStackLayout.Children.Add(frame);
            }
        }

        private void TapPet(int id)
        {
            var searchResultPage = new SeeMyPetProfile(id);

            Navigation.PushAsync(searchResultPage);
        }
        
        private void otherButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OtherPage());
        }

        private void addPetButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPetPage());
        }

        //Following gomb
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FollowersPage(followings));
        }
    }
}