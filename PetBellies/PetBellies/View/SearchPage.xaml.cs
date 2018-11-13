using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
        List<SearchModel> searchModelList = new List<SearchModel>();

        private double currentWidth = 0;
        private double optimalWidth = 0;

        public SearchPage()
        {
            InitializeComponent();

            Device.BeginInvokeOnMainThread(() =>
            {
                hashtagsListStackLayout.IsVisible = false;
                randomPicturesStackLayout.IsVisible = true;

                searchListView.IsRefreshing = true;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            GlobalVariables.CanIGoBackWithTheBackButton = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GlobalVariables.CanIGoBackWithTheBackButton = false;

            if (GlobalVariables.IsPictureDeleted)
            {
                searchListView.IsRefreshing = true;

                searchListView.ItemsSource = new List<SearchModel>();
                
                GlobalVariables.IsPictureDeleted = false;
            }

            InitializeThePetPictures();
        }

        private async void searchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchEntry.Text.Length > 0)
            {
                var list = GlobalVariables.searchFragmentViewModel.GetSearchModelWithKeyword(searchEntry.Text.ToLower(), searchModelList);

                searchListView.ItemsSource = list;
            }
            else
            {
                await Task.Run(() => {
                    SetTheListView();
                });
            }
        }

        private void searchListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;

            var selectedSearchModel = (SearchModel)listView.SelectedItem;

            string hasht = selectedSearchModel.hashtag.Split('#')[1];

            var searchResultPage = new SearchResultPage(selectedSearchModel.petpicturesList, hasht);

            Navigation.PushAsync(searchResultPage);
        }

        private void SetTheListView()
        {
            searchModelList = GlobalVariables.searchFragmentViewModel.GetSearchModel();

            Device.BeginInvokeOnMainThread(() => {
                searchListView.ItemsSource = searchModelList;

                searchListView.IsRefreshing = false;
            });
        }

        async void Handle_Refreshing(object sender, System.EventArgs e)
        {
            await Task.Run(() => {
                SetTheListView();
            });
        }

        private async Task InitializeThePetPictures()
        {
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() => {

                    pictureListGrid.Children.Clear();
                });

                SetTheListView();

                List<Petpictures> petpicturesList = GlobalVariables.searchFragmentViewModel.GetPetpictures();

                currentWidth = Application.Current.MainPage.Width;

                optimalWidth = currentWidth / 3;

                int left = 0;
                int top = 0;

                int i = 1;

                foreach (var item in petpicturesList)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var pet = GlobalVariables.databaseConnection.GetPetByID(item.PetID);

                        if (!GlobalVariables.seeAnOwnerProfileViewModel.IsItABlockedUser(pet.Uploader))
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
                        }
                    });
                }
            });
        }

        public void OnPictureClicked(Petpictures petpictures)
        {
            var isThisMyPet = GlobalVariables.Mypetlist.Where(u => u.petid == petpictures.PetID).FirstOrDefault();

            if (isThisMyPet is null)
            {
                Navigation.PushAsync(new SeeAPicturePage(petpictures));
            }
            else
            {
                Navigation.PushAsync(new SeeMyPicturePage(petpictures));
            }
        }


        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            hashtagsListStackLayout.IsVisible = true;
            randomPicturesStackLayout.IsVisible = false;
        }

        void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (String.IsNullOrEmpty(searchEntry.Text))
            {
                hashtagsListStackLayout.IsVisible = false;
                randomPicturesStackLayout.IsVisible = true;
            }
        }
    }
}