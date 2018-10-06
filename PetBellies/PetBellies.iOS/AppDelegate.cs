using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;

namespace PetBellies.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Custom iOS Tabbar Theme new UIColor(red: 0.91f, green: 0.63f, blue: 0.57f, alpha: 1.0f)
            UITabBar.Appearance.BarTintColor = UIColor.White;
            UITabBar.Appearance.TintColor = new UIColor(red: 0.91f, green: 0.63f, blue: 0.57f, alpha: 1.0f);
            UIProgressView.Appearance.TintColor = UIColor.LightTextColor;

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());
            ImageCircleRenderer.Init();
            // Set app theme

            return base.FinishedLaunching(app, options);
        }
    }
}
