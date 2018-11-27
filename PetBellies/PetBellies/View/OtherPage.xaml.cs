using PetBellies.BLL.FileStoreAndLoad;
using PetBellies.BLL.Helper;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetBellies.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OtherPage : ContentPage
	{
        public OtherPage()
        {
            InitializeComponent();
        }

        private async System.Threading.Tasks.Task deleteAcoountPageButton_ClickedAsync(object sender, EventArgs e)
        {
            loguotButton.IsEnabled = false;
            deleteAcoountPageButton.IsEnabled = false;
            deleteActivity.IsRunning = true;

            string success = GlobalVariables.otherFragmentViewModel.DeleteAccount();

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(), success, English.OK());
            }
            else
            {
                FileStoreAndLoading.InsertToFile(GlobalVariables.logintxt, String.Empty);

                var page = new LoginPage();

                await Navigation.PushModalAsync(new NavigationPage(page)
                {
                    Style = GlobalVariables.NavigationPageStyle
                });
            }
            loguotButton.IsEnabled = true;
            deleteAcoountPageButton.IsEnabled = true;
            deleteActivity.IsRunning = false;
        }

        private async System.Threading.Tasks.Task loguotButton_Clicked(object sender, EventArgs e)
        {
            loguotButton.IsEnabled = false;

            FileStoreAndLoading.InsertToFile(GlobalVariables.logintxt, String.Empty);

            var page = new LoginPage();

            await Navigation.PushModalAsync(new NavigationPage(page)
            {
                Style = GlobalVariables.NavigationPageStyle
            });

            loguotButton.IsEnabled = true;
        }

        private void blockedPeopleButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BlockedPeople());
        }

        private void updateProfileButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdateProfilePage());
        }

    }
}