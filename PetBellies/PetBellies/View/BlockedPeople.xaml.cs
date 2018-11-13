using PetBellies.BLL.Helper;
using PetBellies.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BlockedPeople : ContentPage
	{
		public BlockedPeople ()
		{
			InitializeComponent ();

            InitializeBlockedUserList();
        }

        private void userListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;

            var selectedLVWPAST = (ListViewWithPictureAndSomeText)listView.SelectedItem;

                var searchResultPage = new SeeAnOwnerPage(selectedLVWPAST.user.id);

                Navigation.PushAsync(searchResultPage);
        }

        private void userListView_Refreshing(object sender, EventArgs e)
        {
            InitializeBlockedUserList();
        }

        private async Task InitializeBlockedUserList()
        {
            await Task.Run(()=> {
                Device.BeginInvokeOnMainThread(() => {
                    userListView.ItemsSource = null;
                });

                List<ListViewWithPictureAndSomeText> listViewWithPictureAndSomeText = new List<ListViewWithPictureAndSomeText>();

                var blockedUserList = GlobalVariables.blockedPeopleViewModel.GetBlockedPeoples();

                if (blockedUserList != null && blockedUserList.Count != 0)
                {
                    foreach (var item in blockedUserList)
                    {
                        var blockedUser = GlobalVariables.databaseConnection.GetUserByID(item.BlockedUserID);

                        ListViewWithPictureAndSomeText listViewWith = new ListViewWithPictureAndSomeText()
                        {
                            user = GlobalVariables.databaseConnection.GetUserByID(blockedUser.id),
                            Name = blockedUser.LastName + " " + blockedUser.FirstName,
                            blockedPeople = item
                        };

                        listViewWith.ProfilePicture = ImageSource.FromStream(() => new System.IO.MemoryStream(blockedUser.ProfilePictureURL));

                        listViewWithPictureAndSomeText.Add(listViewWith);
                    }
                }

                Device.BeginInvokeOnMainThread(() => {
                    userListView.ItemsSource = listViewWithPictureAndSomeText;

                    userListView.IsRefreshing = false;
                });
            });
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);

            var cp = ((ListViewWithPictureAndSomeText)mi.CommandParameter);

            var success = GlobalVariables.blockedPeopleViewModel.DeleteBlockedPeople(cp.blockedPeople);

            if (!string.IsNullOrEmpty(success))
                DisplayAlert(English.Failed(), success, English.OK());
            else InitializeBlockedUserList();
        }
    }
}