using PetBellies.BLL.Helper;
using System;
using Xamarin.Essentials;
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

        private async void deleteAcoountPageButton_ClickedAsync(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Delete account", "Are you sure?", "Cancel", "Delete");
            if (action == "Delete")
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
                    await SecureStorage.SetAsync(GlobalVariables.EMAIL_TOKEN, string.Empty);

                    App.SetRootPage(new LoginPage());
                }

                loguotButton.IsEnabled = true;
                deleteAcoountPageButton.IsEnabled = true;
                deleteActivity.IsRunning = false;
            }
        }

        private void loguotButton_Clicked(object sender, EventArgs e)
        {
            loguotButton.IsEnabled = false;

            SecureStorage.SetAsync(GlobalVariables.EMAIL_TOKEN, string.Empty);

            App.SetRootPage(new LoginPage());

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