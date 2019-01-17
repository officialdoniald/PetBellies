using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        private List<Wall> wallList = new List<Wall>();

        public HomePage()
        {
            InitializeComponent();

            Initialize();
        }

        private string followButtonText(bool haveILiked)
        {
            if (haveILiked)
            {
                return GlobalVariables.unlikepng;
            }
            else
            {
                return GlobalVariables.likepng;
            }
        }

        private void Initialize()
        {
            Device.BeginInvokeOnMainThread(() => {
                wallListView.IsRefreshing = true;
                if (Device.OS == TargetPlatform.iOS)
                {
                    SpecialStackLayout.Margin = new Thickness(0, 25, 0, 0);
                }
            });

            GlobalVariables.wallListViewAdapter = new List<WallListViewAdapter>();

            wallList = GlobalVariables.homeFragmentViewModel.GetWallList();

            foreach (var item in wallList)
            {
                if (!GlobalVariables.whosLikedViewModel.IsMyPet(item.petpictures.PetID))
                {
                    GlobalVariables.wallListViewAdapter.Add(new WallListViewAdapter()
                    {
                        wallItem = item,
                        howManyLikes = item.howmanylikes.ToString() + English.GetLike(),
                        petName = item.name,
                        profilepictureURL = ImageSource.FromStream(() => new System.IO.MemoryStream(item.ProfilePictureURL)),
                        pictureURL = ImageSource.FromStream(() => new System.IO.MemoryStream(item.petpictures.PictureURL)),
                        followButtonText = followButtonText(item.haveILiked),
                        hashtags = GlobalVariables.homeFragmentViewModel.GetHashtags(item.petpictures.id)
                    });
                }
            }

            Device.BeginInvokeOnMainThread(() => {
                if (GlobalVariables.wallListViewAdapter.Count == 0)
                {
                    nothingFoundStackLayout.IsVisible = true;
                }
                else
                {
                    nothingFoundStackLayout.IsVisible = false;
                }
                wallListView.ItemsSource = GlobalVariables.wallListViewAdapter;
                wallListView.IsRefreshing = false;
            });
        }

        async void Handle_Refreshing(object sender, System.EventArgs e)
        {
            await Task.Run(() => {
                Initialize();
            });
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var wallListViewAdapterClicked = new WallListViewAdapter();

            try
            {
                wallListViewAdapterClicked = (WallListViewAdapter)(((Label)sender).BindingContext);
            }
            catch (Exception)
            {
                wallListViewAdapterClicked = (WallListViewAdapter)(((Image)sender).BindingContext);
            }

            var isThisMyPet = GlobalVariables.Mypetlist.Where(u => u.petid == wallListViewAdapterClicked.wallItem.petpictures.PetID);

            if (isThisMyPet != null)
            {
                Navigation.PushAsync(new SeeAPetProfile(wallListViewAdapterClicked.wallItem.petpictures.PetID));
            }
            else
            {
                Navigation.PushAsync(new SeeMyPetProfile(wallListViewAdapterClicked.wallItem.petpictures.PetID));
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Label label = (Label)sender;

            var wallListViewAdapterClicked = (WallListViewAdapter)label.BindingContext;

            Navigation.PushAsync(new WhosLiked(wallListViewAdapterClicked.wallItem.petpictures.id));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Image button = (Image)sender;

            var asd = (Grid)button.Parent;

            var collection = (Grid.IGridList<Xamarin.Forms.View>)asd.Children;

            Label likeNumberLabel = (Label)collection[3];

            var wallListViewAdapterClicked = (WallListViewAdapter)button.BindingContext;

            int howmanylikes = wallListViewAdapterClicked.wallItem.howmanylikes;

            if (wallListViewAdapterClicked.wallItem.haveILiked)
            {
                GlobalVariables.homeFragmentViewModel.Unlike(wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Source = GlobalVariables.likepng;

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes - 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString() + English.GetLike();
            }
            else
            {
                GlobalVariables.homeFragmentViewModel.LikePicture(wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Source = GlobalVariables.unlikepng;

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes + 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString() + English.GetLike();
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Image pic = (Image)sender;

            var asd = (Grid)pic.Parent;

            var collection = (Grid.IGridList<Xamarin.Forms.View>)asd.Children;

            Image button = (Image)collection[4];

            Label likeNumberLabel = (Label)collection[3];

            var wallListViewAdapterClicked = (WallListViewAdapter)button.BindingContext;

            int howmanylikes = wallListViewAdapterClicked.wallItem.howmanylikes;

            if (wallListViewAdapterClicked.wallItem.haveILiked)
            {
                GlobalVariables.homeFragmentViewModel.Unlike(wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Source = GlobalVariables.likepng;

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes - 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString() + English.GetLike();
            }
            else
            {
                GlobalVariables.homeFragmentViewModel.LikePicture(wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Source = GlobalVariables.unlikepng;

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes + 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString() + English.GetLike();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GlobalVariables.CanIGoBackWithTheBackButton = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            GlobalVariables.CanIGoBackWithTheBackButton = true;
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var reported = await DisplayActionSheet("More", "Cancel", "Report");

            if (reported == "Report")
            {
                Image pic = (Image)sender;

                var asd = (Grid)pic.Parent;

                var collection = (Grid.IGridList<Xamarin.Forms.View>)asd.Children;

                Image button = (Image)collection[5];

                var wallListViewAdapterClicked = (WallListViewAdapter)button.BindingContext;

                var success = GlobalVariables.seePictureFragmentViewModel.ReportPicture(wallListViewAdapterClicked.wallItem.petpictures.id);

                if (success)
                {
                    try
                    {
                        var user = GlobalVariables.seePictureFragmentViewModel.GetUser(wallListViewAdapterClicked.wallItem.petpictures.PetID);

                        //Aki reportolt
                        string url = string.Format("http://petbellies.com/php/petbelliesreppic.php?email={0}&nev={1}&host={2}", user.Email, user.FirstName, 0);
                        Uri uri = new Uri(url);
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.Method = "GET";
                        WebResponse res = await request.GetResponseAsync();

                        //Akinek a képe van
                        string url1 = string.Format("http://petbellies.com/php/petbelliesreppic.php?email={0}&nev={1}&host={2}", GlobalVariables.ActualUsersEmail, GlobalVariables.ActualUser.FirstName, 1);
                        Uri uri1 = new Uri(url);
                        HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url);
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
        }
    }
}