using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PetBellies.BLL.Helper
{
    public class GenerateItems
    {
        private async Task InitializeThePetPictures(Grid pictureListGrid, Action action, EventHandler Dlegate)
        {
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() => {

                    pictureListGrid.Children.Clear();
                });

                action();

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
                        Image image = new Image();

                        //image.Source = ImageSource.FromUri(new Uri(item.PictureURL));

                        image.Source = new UriImageSource
                        {
                            Uri = new Uri(item.PictureURL),
                            CachingEnabled = true,
                            CacheValidity = new TimeSpan(5, 0, 0, 0)
                        };

                        image.HeightRequest = optimalWidth;

                        image.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 1,
                            TappedCallback = delegate
                            {
                                Dlegate(item);
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
                    });
                }
            });
        }
    }
}
