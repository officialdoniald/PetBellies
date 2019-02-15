using PetBellies.BLL.Helper;
using PetBellies.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FollowersPage : ContentPage
	{
        private int petpicturesid = -1;

        private List<User> users = new List<User>();

        private List<Following> followings = new List<Following>();

        private List<Pet> pets = new List<Pet>();

        private bool isPetFollowingList = true;

        /// <summary>
        /// Ha egy usernek akarjuk kilistázni, hogy kiket followol.
        /// </summary>
        /// <param name="followingList"></param>
        public FollowersPage(List<Following> followingList)
        {
            followings = followingList;

            isPetFollowingList = false;

            InitializeComponent();

            InitializeUserFollowingList();
        }

        /// <summary>
        /// Ha egy petnek akarjuk kilistázni a followerjeit.
        /// </summary>
        /// <param name="petpicturesid"></param>
        public FollowersPage(int petpicturesid)
        {
            this.petpicturesid = petpicturesid;

            isPetFollowingList = true;

            InitializeComponent();

            InitializePetFollowingList();
        }

        private async Task InitializePetFollowingList()
        {
            await Task.Run(() => {
                users = GlobalVariables.followersViewModel.GetUserList(petpicturesid);

                List<ListViewWithPictureAndSomeText> listViewWithPictureAndSomeText = new List<ListViewWithPictureAndSomeText>();

                foreach (var item in users)
                {
                    ListViewWithPictureAndSomeText listViewWith = new ListViewWithPictureAndSomeText()
                    {
                        user = item,
                        Name = item.LastName + " " + item.FirstName
                    };

                    if (item.ProfilePicture != null)
                        listViewWith.ProfilePicture = ImageSource.FromStream(() => new System.IO.MemoryStream(item.ProfilePicture));
                    else listViewWith.ProfilePicture = "";

                    listViewWithPictureAndSomeText.Add(listViewWith);
                }

                Device.BeginInvokeOnMainThread(() => {
                    userListView.ItemsSource = listViewWithPictureAndSomeText;

                    userListView.IsRefreshing = false;
                });
            });
        }

        private async Task InitializeUserFollowingList()
        {
            await Task.Run(() => {
                pets = GlobalVariables.followersViewModel.GetPetList(followings);

                List<ListViewWithPictureAndSomeText> listViewWithPictureAndSomeText = new List<ListViewWithPictureAndSomeText>();

                foreach (var item in pets)
                {
                    ListViewWithPictureAndSomeText listViewWith = new ListViewWithPictureAndSomeText()
                    {
                        pet = item,
                        Name = item.Name
                    };

                    if (item.Profilepicture != null)
                        listViewWith.ProfilePicture = ImageSource.FromStream(() => new System.IO.MemoryStream(item.Profilepicture));
                    else listViewWith.ProfilePicture = "";

                    listViewWithPictureAndSomeText.Add(listViewWith);
                }

                Device.BeginInvokeOnMainThread(() => {
                    userListView.ItemsSource = listViewWithPictureAndSomeText;

                    userListView.IsRefreshing = false;
                });
            });
        }

        private void userListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (isPetFollowingList)
            {
                var listView = (ListView)sender;

                var selectedLVWPAST = (ListViewWithPictureAndSomeText)listView.SelectedItem;

                if (GlobalVariables.ActualUsersEmail != selectedLVWPAST.user.Email)
                {
                    var searchResultPage = new SeeAnOwnerPage(selectedLVWPAST.user.id);

                    Navigation.PushAsync(searchResultPage);
                }
                else
                {
                    var searchResultPage = new MyAccountPage();

                    Navigation.PushAsync(searchResultPage);
                }
            }
            else
            {
                var listView = (ListView)sender;

                var selectedLVWPAST = (ListViewWithPictureAndSomeText)listView.SelectedItem;

                if (GlobalVariables.followersViewModel.IsMyPet(selectedLVWPAST.pet.id))
                {
                    var searchResultPage = new SeeMyPetProfile(selectedLVWPAST.pet.id);

                    Navigation.PushAsync(searchResultPage);
                }
                else
                {
                    var searchResultPage = new SeeAPetProfile(selectedLVWPAST.pet.id);

                    Navigation.PushAsync(searchResultPage);
                }
            }
            
        }

        void Handle_Refreshing(object sender, System.EventArgs e)
        {
            if (isPetFollowingList)
            {
                InitializePetFollowingList();
            }
            else
            {
                InitializeUserFollowingList();
            }
        }
    }
}