using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WeightBuddy.ViewControllers;
using WeightBuddy.Models;

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
            LoadDefaults();

            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var navVC = new UINavigationController();
            var mainVC = new MainViewController();
            Window.RootViewController = navVC;
            navVC.PushViewController(mainVC, false);
            Window.MakeKeyAndVisible();

            return true;
        }

        /// <summary>
        /// If its the first time running the app, load some defaults.
        /// </summary>
        private void LoadDefaults()
        {
            var userDefs = NSUserDefaults.StandardUserDefaults;
            var areInitialValuesSet = userDefs.BoolForKey("Initial Values Set");

            if (!areInitialValuesSet)
            {
                userDefs.SetBool(true, "Initial Values Set");
                userDefs.SetBool(false, "Use Kilograms");
                userDefs.SetBool(false, "Allow100s");
                userDefs.SetBool(false, "Allow50s");
                userDefs.SetBool(true, "Allow45s");
                userDefs.SetBool(true, "Allow35s");
                userDefs.SetBool(true, "Allow25s");
                userDefs.SetBool(false, "Allow20s");
                userDefs.SetBool(false, "Allow15s");
                userDefs.SetBool(false, "Allow12.5s");
                userDefs.SetBool(true, "Allow10s");
                userDefs.SetBool(true, "Allow5s");
                userDefs.SetBool(true, "Allow2.5s");
                userDefs.SetInt(3, "Bar Weight");
                userDefs.Synchronize();
            }
        }
    }
}

