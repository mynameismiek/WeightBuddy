
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
            // TOOD: add settings for kg/lbs and available weights
//            var rightItems = new List<UIBarButtonItem>();
//            rightItems.Add(new UIBarButtonItem(UIBarButtonSystemItem.Action));
//            NavigationItem.SetRightBarButtonItems(rightItems.ToArray(), false);
            // for now...
            _weights.AllowableWeights.AddRange(new List<double>(){ 45, 35, 25, 10, 5, 2.5, 1 });

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
                UpdateTable();
            };

            BarWeightSegmentedControl.ValueChanged += (sender, e) => 
            {
                var selected = BarWeightSegmentedControl.SelectedSegment;

                switch (selected)
                {
                    case 0: // 35
                        _weights.Bar = 35;
                        break;
                    case 1: // 45
                        _weights.Bar = 45;
                        break;
                    default:
                        break;
                }

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

        #endregion

        private void UpdateTable()
        {
            var source = WeightsTableView.Source as WeightsTableViewSource;
            source.Data.Clear();
            source.Data.AddRange(WeightsCalculator.GetWeights(_weights));
            WeightsTableView.ReloadData();
        }
    }
}

