using PetBellies.BLL.Helper;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        private bool wasNotConn = false;

        public MainPage()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.Android)
            {
                On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
                On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarItemColor(Color.Gray);
                On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor(Color.White);
            }

            Title = "MainPage";

            CrossConnectivity.Current.ConnectivityChanged += async (sender, args) =>
            {
                if (!CrossConnectivity.Current.IsConnected && !wasNotConn)
                {
                    wasNotConn = true;

                    await Navigation.PushModalAsync(new NoConnection());
                }
                else
                {
                    wasNotConn = false;
                }
            };

            var homePage = new HomePage();
            var searchPage = new SearchPage();
            var peopleSearchPage = new PeopleSearchPage();
            var uploadPhotoPage = new UploadPhotoPage();
            var myAccountPage = new MyAccountPage();

            Device.BeginInvokeOnMainThread(() =>
            {
                NavigationPage.SetHasBackButton(this, false);
                NavigationPage.SetHasNavigationBar(this, false);

                var navigationHomePage = new NavigationPage(homePage);
                //navigationHomePage.Title = "Főmenü";
                navigationHomePage.Icon = GlobalVariables.homepng;
                navigationHomePage.Style = GlobalVariables.NavigationPageStyle;
                NavigationPage.SetHasNavigationBar(homePage, false);

                var navigationSearchPage = new NavigationPage(searchPage);
                //navigationSearchPage.Title = "Keresés";
                navigationSearchPage.Icon = GlobalVariables.searchpng;
                navigationSearchPage.Style = GlobalVariables.NavigationPageStyle;
                NavigationPage.SetHasNavigationBar(searchPage, false);

                var navigationPeopleSearchPage = new NavigationPage(peopleSearchPage);
                //navigationMyAccountPage.Title = "Fiók";
                navigationPeopleSearchPage.Icon = GlobalVariables.peoplepng;
                navigationPeopleSearchPage.Style = GlobalVariables.NavigationPageStyle;
                NavigationPage.SetHasNavigationBar(peopleSearchPage, false);

                var navigationUploadPhotoPage = new NavigationPage(uploadPhotoPage);
                //navigationUploadPhotoPage.Title = "Fotó";
                navigationUploadPhotoPage.Icon = GlobalVariables.camerapng;
                navigationUploadPhotoPage.Style = GlobalVariables.NavigationPageStyle;
                NavigationPage.SetHasNavigationBar(uploadPhotoPage, false);

                var navigationMyAccountPage = new NavigationPage(myAccountPage);
                //navigationMyAccountPage.Title = "Fiók";
                navigationMyAccountPage.Icon = GlobalVariables.profilepng;
                navigationMyAccountPage.Style = GlobalVariables.NavigationPageStyle;
                NavigationPage.SetHasNavigationBar(myAccountPage, true);

                Children.Add(navigationHomePage);
                Children.Add(navigationSearchPage);
                Children.Add(navigationPeopleSearchPage);
                Children.Add(navigationUploadPhotoPage);
                Children.Add(navigationMyAccountPage);
            });
        }
    }
}