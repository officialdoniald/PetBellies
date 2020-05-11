using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;

namespace PetBellies.Droid
{
    [Activity(Label = "PetBellies", Icon = "@drawable/icon", Theme = "@style/MainTheme", NoHistory = false, MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Window window = this.Window;
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.SetStatusBarColor(Android.Graphics.Color.Rgb(255, 203, 182));
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            //TODO: jobbat kitalálni
            //if (GlobalVariables.CanIGoBackWithTheBackButton)
            //{
            //    base.OnBackPressed();
            //}
        }
    }
}