// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace WeightBuddy.ViewControllers
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField LiftWeightTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel WeightLabel1 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView WeightsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LiftWeightTextField != null) {
				LiftWeightTextField.Dispose ();
				LiftWeightTextField = null;
			}

			if (WeightLabel1 != null) {
				WeightLabel1.Dispose ();
				WeightLabel1 = null;
			}

			if (WeightsTableView != null) {
				WeightsTableView.Dispose ();
				WeightsTableView = null;
			}
		}
	}
}
