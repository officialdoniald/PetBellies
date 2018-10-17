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

        public FollowersPage(int petpicturesid)
        {
            this.petpicturesid = petpicturesid;

            InitializeComponent();

            Initialize();
        }

        private async Task Initialize()
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

                    if (item.ProfilePictureURL != null)
                        listViewWith.ProfilePicture = ImageSource.FromStream(() => new System.IO.MemoryStream(item.ProfilePictureURL));
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

        void Handle_Refreshing(object sender, System.EventArgs e)
        {
            Initialize();
        }
    }
}