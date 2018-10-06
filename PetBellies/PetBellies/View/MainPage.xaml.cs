using PetBellies.BLL.Helper;
using Plugin.Connectivity;
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
	public partial class MainPage : TabbedPage
	{
        private bool wasNotConn = false;

        public MainPage()
        {
            InitializeComponent();

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

            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            
            Device.BeginInvokeOnMainThread(() =>
            {
                var homePage = new HomePage();

                var navigationHomePage = new NavigationPage(homePage);
                //navigationHomePage.Title = "Főmenü";
                navigationHomePage.Icon = GlobalVariables.homepng;
                navigationHomePage.Style = GlobalVariables.NavigationPageStyle;
                NavigationPage.SetHasNavigationBar(homePage, false);

                var searchPage = new SearchPage();
                var navigationSearchPage = new NavigationPage(searchPage);
                //navigationSearchPage.Title = "Keresés";
                navigationSearchPage.Icon = GlobalVariables.searchpng;
                navigationSearchPage.Style = GlobalVariables.NavigationPageStyle;
                NavigationPage.SetHasNavigationBar(searchPage, false);

                var peopleSearchPage = new PeopleSearchPage();
                var navigationPeopleSearchPage = new NavigationPage(peopleSearchPage);
                //navigationMyAccountPage.Title = "Fiók";
                navigationPeopleSearchPage.Icon = GlobalVariables.peoplepng;
                navigationPeopleSearchPage.Style = GlobalVariables.NavigationPageStyle;
                NavigationPage.SetHasNavigationBar(peopleSearchPage, false);

                var uploadPhotoPage = new UploadPhotoPage();
                var navigationUploadPhotoPage = new NavigationPage(uploadPhotoPage);
                //navigationUploadPhotoPage.Title = "Fotó";
                navigationUploadPhotoPage.Icon = GlobalVariables.camerapng;
                navigationUploadPhotoPage.Style = GlobalVariables.NavigationPageStyle;
                NavigationPage.SetHasNavigationBar(uploadPhotoPage, false);

                var myAccountPage = new MyAccountPage();
                var navigationMyAccountPage = new NavigationPage(myAccountPage);
                //navigationMyAccountPage.Title = "Fiók";
                navigationMyAccountPage.Icon = GlobalVariables.profilepng;
                navigationMyAccountPage.Style = GlobalVariables.NavigationPageStyle;
                NavigationPage.SetHasNavigationBar(myAccountPage, false);

                Children.Add(navigationHomePage);
                Children.Add(navigationSearchPage);
                Children.Add(navigationPeopleSearchPage);
                Children.Add(navigationUploadPhotoPage);
                Children.Add(navigationMyAccountPage);
            });
        }
    }
}