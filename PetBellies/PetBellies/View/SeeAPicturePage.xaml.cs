using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PetBellies.BLL.Helper;
using PetBellies.BLL.ViewModel;
using PetBellies.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeeAPicturePage : ContentPage
    {
        private int howmanylike = 0;

        private bool haveiliked = false;

        private List<Likes> likes = new List<Likes>();

        Pet thisPet = new Pet();

        Thickness Thickness = new Thickness();

        Petpictures petpictures = new Petpictures();

        public SeeAPicturePage()
        {
            InitializeComponent();
        }

        public SeeAPicturePage(Petpictures petpictures)
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

                thisPet = GlobalVariables.seePictureFragmentViewModel.GetPetById(petpictures.PetID);

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

                howmanylike = likes.Count;

                Device.BeginInvokeOnMainThread(() =>
                {
                    howmanyLikesLabel.Text = howmanylike.ToString() + English.GetLike();
                });

                haveiliked = GlobalVariables.seePictureFragmentViewModel.HaveILiked(petpictures.id);
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (haveiliked) likeornotImage.Source = GlobalVariables.unlikepng;
                    else likeornotImage.Source = GlobalVariables.likepng;
                });
            });
        }

        private async void likeOrNotButton_ClickedAsync(object sender, EventArgs e)
        {
            if (haveiliked)
            {
                var success = GlobalVariables.seePictureFragmentViewModel.UnLikeClick(petpictures.id);

                if (!String.IsNullOrEmpty(success))
                {
                    await DisplayAlert(English.Failed(), success, English.OK());
                }
                else
                {
                    howmanylike = howmanylike - 1;

                    howmanyLikesLabel.Text = howmanylike.ToString() + English.GetLike();

                    haveiliked = !haveiliked;

                    likeornotImage.Source = GlobalVariables.likepng;
                }
            }
            else
            {
                string success = GlobalVariables.seePictureFragmentViewModel.LikeClick(petpictures.id);

                if (!String.IsNullOrEmpty(success))
                {
                    await DisplayAlert(English.Failed(), success, English.OK());
                }
                else
                {
                    howmanylike = howmanylike + 1;

                    howmanyLikesLabel.Text = howmanylike.ToString() + English.GetLike();

                    haveiliked = !haveiliked;

                    likeornotImage.Source = GlobalVariables.unlikepng;
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeeAPetProfile(petpictures.PetID));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WhosLiked(petpictures.id));
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var reported = await DisplayActionSheet("More", "Cancel", null, "Report");

            if (reported == "Report")
            {
                var success = GlobalVariables.seePictureFragmentViewModel.ReportPicture(petpictures);

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
    }
}