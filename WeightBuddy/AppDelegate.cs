using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WeightBuddy.ViewControllers;

namespace WeightBuddy
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {		
        public override UIWindow Window { get; set; }
		
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var navVC = new UINavigationController();
            var mainVC = new MainViewController();
            Window.RootViewController = navVC;
            navVC.PushViewController(mainVC, false);
            Window.MakeKeyAndVisible();

            return true;
        }
    }
}

