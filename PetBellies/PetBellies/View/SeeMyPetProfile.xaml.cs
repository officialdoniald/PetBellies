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
    public partial class SeeMyPetProfile : ContentPage
    {
        private int petid = -1;

        private double currentWidth = 0;
        private double optimalWidth = 0;

        private Pet thisPet = new Pet();

        private List<Petpictures> petPictureListfromDB = new List<Petpictures>();
        private List<User> followerList = new List<User>();

        private Petpictures petpictures = new Petpictures();

        public SeeMyPetProfile(int petid)
        {
            this.petid = petid;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitializeThePetPictures();
        }

        private void InitializeThePetPictures()
        {
            thisPet = GlobalVariables.ConvertMyPetListToPet(GlobalVariables.Mypetlist.Where(u => u.petid == petid).FirstOrDefault());

            GetFollowers();

            currentWidth = Application.Current.MainPage.Width;

            optimalWidth = currentWidth / 3;

            petnameLabel.Text = thisPet.Name;

            ageLabel.Text = new Segédfüggvények().HowOld(thisPet.Age).ToString();

            kindLabel.Text = thisPet.PetType;

            profilePictureImage.HeightRequest = optimalWidth;
            profilePictureImage.WidthRequest = optimalWidth;

            if (thisPet.Profilepicture != null)
                profilePictureImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(thisPet.Profilepicture));
            else profilePictureImage.Source = "account.png";

            Task.Run(() =>
            {
                GetPetsPictures();
            });
        }

        private void GetPetsPictures()
        {
            int left = 0;
            int top = 0;

            int i = 1;

            var list = GlobalVariables.databaseConnection.GetPetpicturesIDSByPetID(thisPet.id);

            Dictionary<int, int[]> keyValuePairs = new Dictionary<int, int[]>();

            foreach (var item in list)
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

            foreach (var item in list)
            {
                Task.Run(() =>
                {
                    var petpicture = GlobalVariables.databaseConnection.GetOnePetpicturesByID(item);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Image image = new Image();

                        image.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(petpicture.PictureURL));

                        image.HeightRequest = optimalWidth;

                        image.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 1,
                            TappedCallback = delegate
                            {
                                OnPictureClicked(petpicture);
                            }
                        });

                        image.Aspect = Aspect.AspectFill;

                        pictureListGrid.Children.Add(image, keyValuePairs[item][0], keyValuePairs[item][1]);
                    });
                });
            }
        }

        private void GetFollowers()
        {
            Task.Run(() =>
            {
                followerList = GlobalVariables.followersViewModel.GetUserList(this.petid);

                Device.BeginInvokeOnMainThread(() =>
                {
                    followersLabel.Text = followerList.Count.ToString();
                    followersLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        NumberOfTapsRequired = 1,
                        TappedCallback = (arg1, arg2) => Handle_Tapped(followersLabel, null)
                    });
                    followersTextLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        NumberOfTapsRequired = 1,
                        TappedCallback = (arg1, arg2) => Handle_Tapped(followersLabel, null)
                    });

                });
            });
        }

        public void OnPictureClicked(Petpictures petpictures)
        {
            Navigation.PushAsync(new SeeMyPicturePage(petpictures));
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new FollowersPage(followerList));
        }

        private async void MoreToolbarItem_Activated(object sender, EventArgs e)
        {
            var reported = await DisplayActionSheet("More", "Cancel", null, "Edit");

            if (reported == "Edit")
            {
                await Navigation.PushAsync(new UpdatePetProfilePage(thisPet.id));
            }
        }
    }
}