
using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using iOSLib;
using WeightBuddy.DataSources;
using WeightBuddy.Models;
using WeightBuddy.Calculators;
using MonoTouch.ObjCRuntime;

namespace WeightBuddy.ViewControllers
{
    /// <summary>
    /// Main view controller.
    /// </summary>
    public partial class MainViewController : UIViewController
    {
        private Weights _weights;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightBuddy.ViewControllers.MainViewController"/> class.
        /// </summary>
        public MainViewController()
            : base("MainView", null)
        {
            _weights = new Weights();
        }

        #region View Life Cycle

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            NSNotificationCenter.DefaultCenter.PostNotificationName(ViewExtensions.ViewDisposed, View);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            NavigationItem.Title = "Weight Buddy";
            var rightItems = new List<UIBarButtonItem>();
            var settingsBtn = new UIBarButtonItem(UIBarButtonSystemItem.Action); 
            settingsBtn.Action = new Selector("showSettings:");
            settingsBtn.Target = this;
            rightItems.Add(settingsBtn);
            NavigationItem.SetRightBarButtonItems(rightItems.ToArray(), false);

            var source = new WeightsTableViewSource();
            WeightsTableView.Source = source;
            WeightsTableView.ReloadData();

            LiftWeightTextField.EditingChanged += (sender, e) => 
            {
                var desired = 0;
                if (!int.TryParse(LiftWeightTextField.Text, out desired))
                {
                    LiftWeightTextField.Layer.BorderColor = UIColor.Red.CGColor;
                    return;
                }

                LiftWeightTextField.Layer.BorderColor = UIColor.Black.CGColor;
                _weights.DesiredLoad = desired;
                var userDefs = NSUserDefaults.StandardUserDefaults;
                userDefs.SetInt(desired, "Desired Lift");
                UpdateTable();                
            };

            View.Tap(1).Event += (sender, recognizer) =>
            {
                var firstResponder = this.GetUITextFieldFirstResponder();
                if (firstResponder != null)
                {
                    firstResponder.EndEditing(true);
                }
            };
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var userDefs = NSUserDefaults.StandardUserDefaults;
            WeightLabel1.Text = userDefs.BoolForKey("Use Kilograms") ? "kg" : "lbs";
            _weights.AllowableWeights.Clear();
            _weights.AllowableWeights.AddRange(GetAllowedWeights());            
            LiftWeightTextField.Text = userDefs.IntForKey("Desired Lift").ToString();
            LiftWeightTextField.SendActionForControlEvents(UIControlEvent.EditingChanged);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        #endregion

        /// <summary>
        /// Update the table of weights.
        /// </summary>
        private void UpdateTable()
        {
            _weights.Bar = GetBarWeight();
            var source = WeightsTableView.Source as WeightsTableViewSource;
            source.Data.Clear();
            source.Data.AddRange(WeightsCalculator.GetWeights(_weights));
            WeightsTableView.ReloadData();
        }

        /// <summary>
        /// Show the settings view.
        /// </summary>
        [Action("showSettings:")]
        public void ShowSettings()
        {
            NavigationController.PushViewController(new SettingsViewController(), true);
        }

        /// <summary>
        /// Get a list of allowed weights based on what the user has set.
        /// </summary>
        /// <returns>The allowed weights.</returns>
        private double[] GetAllowedWeights()
        {
            var weights = new List<double>();
            var userDefs = NSUserDefaults.StandardUserDefaults;
            var useKg = userDefs.BoolForKey("Use Kilograms");

            if (userDefs.BoolForKey("Allow100s"))
            {
                weights.Add(useKg ? 45.0 : 100.0);
            }

            if (userDefs.BoolForKey("Allow50s"))
            {
                weights.Add(useKg ? 23.0 : 50.0);
            }

            if (userDefs.BoolForKey("Allow45s"))
            {
                weights.Add(useKg ? 20.0 : 45.0);
            }

            if (userDefs.BoolForKey("Allow35s"))
            {
                weights.Add(useKg ? 16.0 : 35.0);
            }

            if (userDefs.BoolForKey("Allow25s"))
            {
                weights.Add(useKg ? 11.0 : 25.0);
            }

            if (userDefs.BoolForKey("Allow20s"))
            {
                weights.Add(useKg ? 9.0 : 20.0);
            }

            if (userDefs.BoolForKey("Allow15s"))
            {
                weights.Add(useKg ? 7.0 : 15.0);
            }

            if (userDefs.BoolForKey("Allow12p5s"))
            { 
                weights.Add(useKg ? 6.0 : 12.5);
            }

            if (userDefs.BoolForKey("Allow10s"))
            {
                weights.Add(useKg ? 5.0 : 10.0);
            }

            if (userDefs.BoolForKey("Allow5s"))
            {
                weights.Add(useKg ? 2.0 : 5.0);
            }

            if (userDefs.BoolForKey("Allow2p5s"))
            {
                weights.Add(useKg ? 1.0 : 2.5);
            }

            return weights.ToArray();
        }

        /// <summary>
        /// Get the bar weight. 
        /// </summary>
        /// <returns>The bar weight.</returns>
        private int GetBarWeight()
        {
            int barWeight;
            var userDefs = NSUserDefaults.StandardUserDefaults;
            var useKg = userDefs.BoolForKey("Use Kilograms");
            var segment = userDefs.IntForKey("Bar Weight");

            switch (segment)
            {
                case 0: // 5/10
                    barWeight = useKg ? 5 : 10;
                    break;
                case 1: // 10/25
                    barWeight = useKg ? 10 : 25;
                    break;
                case 2: // 15/35
                    barWeight = useKg ? 15 : 35;
                    break;
                default: // 20/45
                    barWeight = useKg ? 20 : 45;
                    break;
            }

            return barWeight;
        }
    }
}

