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
	public partial class JustActivityIndicator : ContentPage
	{
        private string isEmpty = string.Empty;

        public JustActivityIndicator()
        {
            InitializeComponent();
        }

        public JustActivityIndicator(string facebookOrLogin)
        {
            InitializeComponent();

            isEmpty = facebookOrLogin;
        }

        public JustActivityIndicator(bool connection)
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GlobalVariables.InitializeGlobalCasualImage();
            
            Task.Run(()=>
            {
                if (!string.IsNullOrEmpty(isEmpty))
                {
                    GlobalVariables.InitializeUsersEmailVariable();

                    GlobalVariables.InitializeUser();

                    GlobalVariables.Mypetlist = new List<MyPetsList>();

                    GlobalVariables.GetMyPets();

                    GlobalVariables.SetMyPetListString();

                    Device.BeginInvokeOnMainThread(()=> {
                        Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                    });
                }
                else
                {
                    GlobalVariables.GetMyPets();

                    GlobalVariables.SetMyPetListString();

                    Device.BeginInvokeOnMainThread(() => {
                        Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                    });
                }
            });
        }
    }
}