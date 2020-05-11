using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            GlobalEvents.OnFollowUser += GlobalEvents_OnFollowUser;
            GlobalEvents.OnUnFollowUser += GlobalEvents_OnUnFollowUser;

            Initialize();
        }

        private void GlobalEvents_OnUnFollowUser(object sender, object e)
        {
            Task.Run(async () =>
            {
                GlobalVariables.WallStartIndex = 0;

                await Initialize();
            });
        }

        private void GlobalEvents_OnFollowUser(object sender, object e)
        {
            Task.Run(async () =>
            {
                GlobalVariables.WallStartIndex = 0;

                await Initialize();
            });
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

        private async Task Initialize()
        {
            wallListView.IsRefreshing = true;

            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        SpecialStackLayout.Margin = new Thickness(0, 25, 0, 0);
                    }
                });

                GlobalVariables.wallListViewAdapter = new ObservableCollection<WallListViewAdapter>();
                
                AddWallListItem();
            });
        }

        private void Handle_Refreshing(object sender, System.EventArgs e)
        {
            Task.Run(async () =>
            {
                GlobalVariables.WallStartIndex = 0;

                await Initialize();
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

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var reported = await DisplayActionSheet("More", "Cancel", null, "Report");

            if (reported == "Report")
            {
                Image pic = (Image)sender;

                var asd = (Grid)pic.Parent;

                var collection = (Grid.IGridList<Xamarin.Forms.View>)asd.Children;

                Image button = (Image)collection[5];

                var wallListViewAdapterClicked = (WallListViewAdapter)button.BindingContext;

                var success = GlobalVariables.seePictureFragmentViewModel.ReportPicture(wallListViewAdapterClicked.wallItem.petpictures);

                if (success)
                {
                    await DisplayAlert("Success", "Thanks..", "OK");
                }
                else
                {
                    await DisplayAlert("Failed", "Something went wrong", "OK");
                }
            }
        }

        private void MoreButton_Clicked(object sender, EventArgs e)
        {
            GlobalVariables.WallStartIndex += GlobalVariables.WallCount;
            
            wallListView.IsRefreshing = true;

            AddWallListItem();
        }

        private async void AddWallListItem()
        {
            await Task.Run(() =>
            {
                wallList = GlobalVariables.homeFragmentViewModel.GetWallList();

                if (wallList.Count == 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MoreButton.IsVisible = false;
                    });
                }

                foreach (var item in wallList)
                {
                    var hastags = GlobalVariables.homeFragmentViewModel.GetHashtags(item.petpictures.id);

                    GlobalVariables.wallListViewAdapter.Add(new WallListViewAdapter()
                    {
                        wallItem = item,
                        howManyLikes = item.howmanylikes.ToString() + English.GetLike(),
                        petName = item.name,
                        profilepictureURL = ImageSource.FromStream(() => new System.IO.MemoryStream(item.ProfilePictureURL)),
                        pictureURL = ImageSource.FromStream(() => new System.IO.MemoryStream(item.petpictures.PictureURL)),
                        followButtonText = followButtonText(item.haveILiked),
                        hashtags = hastags
                    });
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    if (GlobalVariables.wallListViewAdapter.Count == 0)
                    {
                        wallListView.IsVisible = false;
                        nothingFoundStackLayout.IsVisible = true;
                    }
                    else
                    {
                        wallListView.IsVisible = true;
                        nothingFoundStackLayout.IsVisible = false;
                    }

                    wallListView.ItemsSource = GlobalVariables.wallListViewAdapter;

                    wallListView.IsRefreshing = false;
                });
            });
        }
    }
}