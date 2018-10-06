using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchResultPage : ContentPage
	{
        List<Petpictures> petpicturesList =
            new List<Petpictures>();

        public SearchResultPage()
        {
            InitializeComponent();
        }

        public SearchResultPage(List<Petpictures> petpicturesList, string hashtag)
        {
            this.Title = "#" + hashtag;

            this.petpicturesList = petpicturesList;

            InitializeComponent();

            Initialize();
        }

        private async Task Initialize()
        {
            await Task.Run(() => {
                Device.BeginInvokeOnMainThread(() => {
                    var currentWidth = Application.Current.MainPage.Width;

                    var optimalWidth = currentWidth / 3;

                    int left = 0;
                    int top = 0;

                    int i = 1;

                    foreach (var item in petpicturesList)
                    {
                        Image image = new Image();

                        //image.Source = ImageSource.FromUri(new Uri(item.PictureURL));

                        image.Source = new UriImageSource
                        {
                            Uri = new Uri(item.PictureURL),
                            CachingEnabled = true,
                            CacheValidity = new TimeSpan(7, 0, 0, 0)
                        };

                        image.HeightRequest = optimalWidth;

                        image.Aspect = Aspect.AspectFill;

                        image.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 1,
                            TappedCallback = delegate
                            {
                                OnPictureClicked(item);
                            }
                        });

                        searchResultGrid.Children.Add(image, top, left);

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
            });
        }

        protected override void OnAppearing()
        {
            if (GlobalVariables.IsPictureDeleted)
            {
                Initialize();
            }
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
    }
}