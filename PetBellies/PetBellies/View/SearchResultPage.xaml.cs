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
        List<int> petpicturesList =
            new List<int>();

        private string hashtag = string.Empty;

        public SearchResultPage()
        {
            InitializeComponent();
        }

        public SearchResultPage(string hashtag)
        {
            this.hashtag = hashtag;

            this.Title = "#" + hashtag;

            InitializeComponent();

            Initialize();
        }

        private async Task Initialize()
        {
            var currentWidth = Application.Current.MainPage.Width;

            var optimalWidth = currentWidth / 3;

            await Task.Run(() =>
            {
                int left = 0;
                int top = 0;

                int i = 1;

                petpicturesList = GlobalVariables.databaseConnection.GetPetpicturesByHashtags(hashtag);

                Dictionary<int, int[]> keyValuePairs = new Dictionary<int, int[]>();

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
                        var item = GlobalVariables.databaseConnection.GetOnePetpicturesByID(petpictureid);

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

                            searchResultGrid.Children.Add(image, keyValuePairs[petpictureid][0], keyValuePairs[petpictureid][1]);
                        });
                    });
                }
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
            if (!GlobalVariables.databaseConnection.GetPetpicturesExistByPetPicturesID(petpictures.id))
            {
                Navigation.PushAsync(new NoPictureFoundPage());
            }
            else
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
}