using System;
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
            UITabBar.Appearance.BarTintColor = UIColor.White;
            UITabBar.Appearance.TintColor = new UIColor(red: 1.0f, green: 0.80f, blue: 0.71f, alpha: 1.0f);
            UIProgressView.Appearance.TintColor = UIColor.LightTextColor;
            //var titleAttribute = new UITextAttributes { Font = GetFont(50) };
            //// Updates the ToolbarItem appearance.
            //UIBarButtonItem.Appearance.SetTitleTextAttributes(titleAttribute, UIControlState.Normal);
            //// Sets the NavigationBar title items. Also sets the format for TabbedPage tab items for some reason.
            //UINavigationBar.Appearance.SetTitleTextAttributes(titleAttribute);

            var statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                statusBar.BackgroundColor = new UIColor(red: 1.0f, green: 0.80f, blue: 0.71f, alpha: 1.0f);
                statusBar.TintColor = UIColor.White;
            }

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());
            ImageCircleRenderer.Init();

            return base.FinishedLaunching(app, options);
        }

        public static UIFont GetFont(nfloat fontSize)
        {
            return UIFont.FromName(StandardFontName, fontSize);
        }


        public static string StandardFontName => "AvenirNextCondensed-Regular";
    }
}
