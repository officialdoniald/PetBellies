using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetBellies.BLL.Helper;
using PetBellies.BLL.ViewModel;
using PetBellies.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SeeMyPicturePage : ContentPage
	{
        private int howmanylike = 0;

        private List<Likes> likes = new List<Likes>();

        Pet thisPet = new Pet();

        Thickness Thickness = new Thickness();

        Petpictures petpictures = new Petpictures();

        public SeeMyPicturePage(Petpictures petpictures)
        {
            this.petpictures = petpictures;

            InitializeComponent();

            Thickness.Bottom = 0;
            Thickness.Left = 0;
            Thickness.Right = 0;
            Thickness.Top = 0;

            Initialize();
        }

        private void Initialize()
        {
            Task.Run(() =>
            {
                thisPet = GlobalVariables.ConvertMyPetListToPet(GlobalVariables.Mypetlist.Where(u => u.petid == petpictures.PetID).FirstOrDefault());
                Device.BeginInvokeOnMainThread(() =>
                {
                    nameLabel.Text = thisPet.Name;

                    profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(thisPet.Profilepicture));

                    pictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(petpictures.PictureURL));
                });

                var asd = GlobalVariables.seePictureFragmentViewModel.GetHashtags(petpictures.id).Split(' ');

                foreach (var item2 in asd)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Label hashtagLabel = new Label()
                        {
                            Text = item2,
                            Style = GlobalVariables.NormalLabel,
                            Margin = Thickness
                        };

                        var onHashtagClickedTap = new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 1
                        };
                        onHashtagClickedTap.Tapped += (s, e) =>
                        {
                            Navigation.PushAsync(new SearchResultPage(item2.Split('#')[1]));
                        };

                        hashtagLabel.GestureRecognizers.Add(onHashtagClickedTap);

                        mainStackLayout.Children.Add(hashtagLabel);
                    });
                }

                likes = GlobalVariables.seePictureFragmentViewModel.GetLikes(petpictures.id);

                Device.BeginInvokeOnMainThread(() =>
                {
                    howmanylike = likes.Count;

                    howmanyLikesLabel.Text = howmanylike.ToString() + English.GetLike();
                });
            });
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeeMyPetProfile(petpictures.PetID));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WhosLiked(petpictures.id));
        }

        private async void MoreToolbarItem_Activated(object sender, EventArgs e)
        {
            var reported = await DisplayActionSheet("More", "Cancel", "Delete");

            if (reported == "Delete")
            {
                Task.Run(async ()=> {
                    if (!GlobalVariables.seePictureFragmentViewModel.DeletePicture(this.petpictures))
                    {
                        await DisplayAlert(English.Failed(), English.SomethingWentWrong(), English.OK());
                    }
                    else
                    {
                        GlobalVariables.IsPictureDeleted = true;

                        await Navigation.PopToRootAsync();

                        await DisplayAlert(English.Successful(), English.SuccessfulDeletedThePicture(), English.OK());
                    }
                });
            }
        }
    }
}