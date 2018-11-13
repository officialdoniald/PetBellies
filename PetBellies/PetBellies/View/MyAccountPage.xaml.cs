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
                userNameLabel.Text = GlobalVariables.ActualUser.FirstName + " " + GlobalVariables.ActualUser.LastName;
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

            userNameLabel.Text = GlobalVariables.ActualUser.FirstName + " " + GlobalVariables.ActualUser.LastName;

            followings = GlobalVariables.myAccountPageViewModel.GetMyFollowing();

            followingLabel.Text = followings.Count + " following";

            listViewWithPictureAndSomeText = new List<ListViewWithPictureAndSomeText>();

            foreach (var item in GlobalVariables.Mypetlist)
            {
                listViewWithPictureAndSomeText.Add(new ListViewWithPictureAndSomeText()
                {
                    pet = GlobalVariables.ConvertMyPetListToPet(item),
                    ProfilePicture = item.ProfilePictureURL == null ? "" : ImageSource.FromStream(() => new System.IO.MemoryStream(item.ProfilePictureURL)),
                    Name = item.Name
                });
            }

            petListView.ItemsSource = listViewWithPictureAndSomeText;
        }

        private void petListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;

            var selectedLVWPAST = (ListViewWithPictureAndSomeText)listView.SelectedItem;

            var searchResultPage = new SeeMyPetProfile(selectedLVWPAST.pet.id);

            Navigation.PushAsync(searchResultPage);
        }

        private void otherButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OtherPage());
        }

        private void updateProfileButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdateProfilePage());
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