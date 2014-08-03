
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WeightBuddy.Models;

namespace WeightBuddy.ViewControllers
{
    /// <summary>
    /// Settings view controller.
    /// </summary>
    public partial class SettingsViewController : UIViewController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeightBuddy.ViewControllers.SettingsViewController"/> class.
        /// </summary>
        public SettingsViewController()
            : base("SettingsView", null)
        {
        }

        #region View Life Cycle

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitFromUserDefaults();
            NavigationItem.Title = "Settings";
            UseKilogramsSwitch.TouchUpInside += (sender, e) => UpdateUnits(UseKilogramsSwitch.On);
            ScrollView.ContentSize = ContentView.Frame.Size;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            ScrollView.FlashScrollIndicators();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            // save the user's preferences
            var userDefs = NSUserDefaults.StandardUserDefaults;
            userDefs.SetBool(UseKilogramsSwitch.On, "Use Kilograms");
            userDefs.SetBool(Allow100sSwitch.On, "Allow100s");
            userDefs.SetBool(Allow50sSwitch.On, "Allow50s");
            userDefs.SetBool(Allow45sSwitch.On, "Allow45s");
            userDefs.SetBool(Allow35sSwitch.On, "Allow35s");
            userDefs.SetBool(Allow25sSwitch.On, "Allow25s");
            userDefs.SetBool(Allow20sSwitch.On, "Allow20s");
            userDefs.SetBool(Allow15sSwitch.On, "Allow15s");
            userDefs.SetBool(Allow12p5sSwitch.On, "Allow12.5s");
            userDefs.SetBool(Allow10sSwitch.On, "Allow10s");
            userDefs.SetBool(Allow5sSwitch.On, "Allow5s");
            userDefs.SetBool(Allow2p5sSwitch.On, "Allow2.5s");
            userDefs.SetInt(BarWeightSegmentedControl.SelectedSegment, "Bar Weight");
            userDefs.Synchronize();
        }

        #endregion

        /// <summary>
        /// Loads the values from the User Defaults and sets the controls accordinly.
        /// </summary>
        private void InitFromUserDefaults()
        {
            var userDefs = NSUserDefaults.StandardUserDefaults;
            var useKg = userDefs.BoolForKey("Use Kilograms");
            UseKilogramsSwitch.On = useKg;
            BarWeightSegmentedControl.SelectedSegment = userDefs.IntForKey("Bar Weight");
            Allow100sSwitch.On = userDefs.BoolForKey("Allow100s");
            Allow50sSwitch.On = userDefs.BoolForKey("Allow50s");
            Allow45sSwitch.On = userDefs.BoolForKey("Allow45s");
            Allow35sSwitch.On = userDefs.BoolForKey("Allow35s");
            Allow25sSwitch.On = userDefs.BoolForKey("Allow25s");
            Allow20sSwitch.On = userDefs.BoolForKey("Allow20s");
            Allow15sSwitch.On = userDefs.BoolForKey("Allow15s");
            Allow12p5sSwitch.On = userDefs.BoolForKey("Allow12.5s");
            Allow10sSwitch.On = userDefs.BoolForKey("Allow10s");
            Allow5sSwitch.On = userDefs.BoolForKey("Allow5s");
            Allow2p5sSwitch.On = userDefs.BoolForKey("Allow2.5s");
            BarWeightSegmentedControl.SelectedSegment = userDefs.IntForKey("Bar Weight");

            UpdateUnits(useKg);
        }

        /// <summary>
        /// Updates the units displayed on this screen.
        /// </summary>
        /// <param name="useKg">If set to <c>true</c> use kg.</param>
        private void UpdateUnits(bool useKg)
        {
            Allow100sLabel.Text = useKg ? "45 kg" : "100 lbs";
            Allow50sLabel.Text = useKg ? "23 kg" : "50 lbs";
            Allow45sLabel.Text = useKg ? "20 kg" : "45 lbs";
            Allow35sLabel.Text = useKg ? "16 kg" : "35 lbs";
            Allow25sLabel.Text = useKg ? "11 kg" : "25 lbs";
            Allow20sLabel.Text = useKg ? "9 kg" : "20 lbs";
            Allow15sLabel.Text = useKg ? "7 kg" : "15 lbs";
            Allow12p5sLabel.Text = useKg ? "6 kg" : "12.5 lbs";
            Allow10sLabel.Text = useKg ? "5 kg" : "10 lbs";
            Allow5sLabel.Text = useKg ? "2 kg" : "5 lbs";
            Allow2p5sLabel.Text = useKg ? "1 kg" : "2.5 lbs";
            BarWeightLabel.Text = useKg ? "Bar Weight (kg)" : "Bar Weight (lbs)";
            BarWeightSegmentedControl.SetTitle(useKg ?  "5" : "10", 0);
            BarWeightSegmentedControl.SetTitle(useKg ? "10" : "25", 1);
            BarWeightSegmentedControl.SetTitle(useKg ? "15" : "35", 2);
            BarWeightSegmentedControl.SetTitle(useKg ? "20" : "45", 3);
        }
    }
}

