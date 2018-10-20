using PetBellies.BLL.Helper;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PetBellies
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            GlobalVariables.GlobalPassword = PetBellies.Properties.Resources.ResourceManager.GetString("GlobalPassword");
            GlobalVariables.AzureDBConnectionString = PetBellies.Properties.Resources.ResourceManager.GetString("AzureDBConnectionString");
            GlobalVariables.NormalLabel = (Style)Resources["NormalLabel"];
            GlobalVariables.NavigationPageStyle = (Style)Resources["NavigationPageStyle"];

            if (!CrossConnectivity.Current.IsConnected)
            {
                while (!CrossConnectivity.Current.IsConnected) { }
            }
            GlobalVariables.InitializeUsersEmail();

            if (!GlobalVariables.HaveToLogin)
            {
                GlobalVariables.InitializeUser();

                MainPage = new View.JustActivityIndicator();
            }
            else
            {
                var page = new View.LoginPage();

                MainPage = new NavigationPage(page)
                {
                    Style = GlobalVariables.NavigationPageStyle
                };

                NavigationPage.SetHasNavigationBar(page, false);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
