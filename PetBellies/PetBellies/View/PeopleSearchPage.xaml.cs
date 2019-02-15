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
	public partial class PeopleSearchPage : ContentPage
	{
        List<UserJustWithPicAndName> userJustWithPicAndName = new List<UserJustWithPicAndName>();

        private bool isItFinished = false;

        public PeopleSearchPage()
        {
            InitializeComponent();

            //Itt letölthetné az első 10et vagy randomba letölthetne 10et 
            //es azt jelenítené meg
            Device.BeginInvokeOnMainThread(()=> 
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    SpecialStackLayout.Margin = new Thickness(0, 50, 0, 0);
                }
            });

            Initialize();
        }

        private async Task Initialize()
        {
            await Task.Run(() =>
            {
                var list = GlobalVariables.peopleSearchPageViewModel.GetUserWithKeyword("");

                userJustWithPicAndName = new List<UserJustWithPicAndName>();

                foreach (var item in list)
                {
                    userJustWithPicAndName.Add(new UserJustWithPicAndName()
                    {
                        Name = item.FirstName + " " + item.LastName,
                        id = item.id,
                        ProfilePicture = item.ProfilePicture == null ? "" : ImageSource.FromStream(() => new System.IO.MemoryStream(item.ProfilePicture)),
                        Email = item.Email
                    });
                }

                isItFinished = true;

                Device.BeginInvokeOnMainThread(() =>
                {
                    userListView.IsRefreshing = false;

                    userListView.ItemsSource = userJustWithPicAndName;
                });
            });
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            SetList();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;

            var selectedLVWPAST = (UserJustWithPicAndName)listView.SelectedItem;

            if (GlobalVariables.ActualUsersEmail != selectedLVWPAST.Email)
            {
                var searchResultPage = new SeeAnOwnerPage(selectedLVWPAST.id);

                Navigation.PushAsync(searchResultPage);
            }
            else
            {
                var searchResultPage = new MyAccountPage();

                Navigation.PushAsync(searchResultPage);
            }
        }

        void Handle_Completed(object sender, System.EventArgs e)
        {
            SetList();
        }

        private void SetList()
        {
            if (!String.IsNullOrEmpty(searchEntry.Text) && searchEntry.Text.Length >= 3 && isItFinished)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    userListView.IsRefreshing = true;
                });

                userListView.ItemsSource = GlobalVariables.peopleSearchPageViewModel.GetUserByKeyWord(searchEntry.Text, userJustWithPicAndName);
            }
            else
            {
                userListView.ItemsSource = userJustWithPicAndName;
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                userListView.IsRefreshing = false;
            });
        }

        async void Handle_Refreshing(object sender, System.EventArgs e)
        {
            await Initialize();

            SetList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GlobalVariables.CanIGoBackWithTheBackButton = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            GlobalVariables.CanIGoBackWithTheBackButton = true;
        }
    }
}