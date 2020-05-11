using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private ObservableCollection<SearchModel> searchModelList = new ObservableCollection<SearchModel>();

        private List<int> petpicturesList = new List<int>();

        private Dictionary<int, int[]> keyValuePairs = new Dictionary<int, int[]>();

        private int top = 0;
        private int left = 0;
        private int i = 1;

        private double currentWidth = 0;
        private double optimalWidth = 0;

        public SearchPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    searchListView.IsVisible = false;
                    pictureListGrid.IsVisible = true;

                    if (Device.OS == TargetPlatform.iOS)
                    {
                        SpecialStackLayout.Margin = new Thickness(0, 50, 0, 0);
                    }

                    searchListView.IsRefreshing = true;
                });

                ListView_Refreshing(this, null);
            });
        }

        private void searchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchEntry.Text.Length > 0)
            {
                var list = GlobalVariables.searchFragmentViewModel.GetSearchModelWithKeyword(searchEntry.Text.ToLower(), searchModelList);

                searchListView.ItemsSource = list;
            }
            else
            {
                Task.Run(() =>
                {
                    SetTheListView();
                });
            }
        }

        private void searchListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;

            var selectedSearchModel = (SearchModel)listView.SelectedItem;

            string hasht = selectedSearchModel.hashtag.Split('#')[1];

            var searchResultPage = new SearchResultPage(hasht);

            Navigation.PushAsync(searchResultPage);
        }

        private void SetTheListView()
        {
            searchModelList = GlobalVariables.searchFragmentViewModel.GetSearchModel();

            Device.BeginInvokeOnMainThread(() =>
            {
                searchListView.ItemsSource = searchModelList;

                searchListView.IsRefreshing = false;
            });
        }

        private void Handle_Refreshing(object sender, System.EventArgs e)
        {
            Task.Run(() =>
            {
                SetTheListView();
            });
        }

        private void InitializeThePetPictures()
        {
            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    pictureListGrid.Children.Clear();
                });

                SetTheListView();

                top = 0;
                left = 0;
                i = 1;

                GlobalVariables.PetPicturesStartIndex = 0;

                petpicturesList = new List<int>();

                currentWidth = Application.Current.MainPage.Width;

                optimalWidth = currentWidth / 3;

                keyValuePairs = new Dictionary<int, int[]>();

                FillGrid();
            });
        }

        public void OnPictureClicked(Petpictures petpictures)
        {
            Task.Run(() =>
            {
                if (!GlobalVariables.databaseConnection.GetPetpicturesExistByPetPicturesID(petpictures.id))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PushAsync(new NoPictureFoundPage());
                    });
                }
                else
                {
                    var isThisMyPet = GlobalVariables.Mypetlist.Where(u => u.petid == petpictures.PetID).FirstOrDefault();

                    if (isThisMyPet is null)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PushAsync(new SeeAPicturePage(petpictures));
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Navigation.PushAsync(new SeeMyPicturePage(petpictures));
                        });
                    }
                }
            });
        }

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            searchListView.IsVisible = true;
            pictureListGrid.IsVisible = false;
        }

        void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (String.IsNullOrEmpty(searchEntry.Text))
            {
                searchListView.IsVisible = false;
                pictureListGrid.IsVisible = true;
            }
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                if (GlobalVariables.IsPictureDeleted)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        searchListView.IsRefreshing = true;

                        searchListView.ItemsSource = new List<SearchModel>();
                    });

                    GlobalVariables.IsPictureDeleted = false;
                }

                InitializeThePetPictures();

                try
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        pictureListView.IsRefreshing = false;
                    });
                }
                catch (Exception) { }
            });
        }

        private void MoreButton_Clicked(object sender, EventArgs e)
        {
            ((ListView)((Button)sender).Parent).IsRefreshing = true;

            Task.Run(() =>
            {
                GlobalVariables.PetPicturesStartIndex += GlobalVariables.PetPicturesCount;

                FillGrid();

                Device.BeginInvokeOnMainThread(() =>
                {
                    ((ListView)((Button)sender).Parent).IsRefreshing = false;
                });
            });
        }

        public void FillGrid()
        {
            Task.Run(() =>
            {
                petpicturesList = GlobalVariables.searchFragmentViewModel.GetPetpictures();

                if (petpicturesList != null && petpicturesList.Count != 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MoreButton.IsVisible = true;
                    });

                    foreach (var item in petpicturesList)
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

                    foreach (var petpictureid in petpicturesList)
                    {
                        Task.Run(() =>
                        {
                            var item = GlobalVariables.databaseConnection.GetPetPictureByID(petpictureid);

                            Image image = new Image();

                            Device.BeginInvokeOnMainThread(() =>
                            {
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

                                pictureListGrid.Children.Add(image, keyValuePairs[petpictureid][0], keyValuePairs[petpictureid][1]);
                            });
                        });
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MoreButton.IsVisible = false;
                    });
                }
            });
        }
    }
}