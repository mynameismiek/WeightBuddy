using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace WeightBuddy.DataSources
{
    /// <summary>
    /// Weights table view source.
    /// </summary>
    public class WeightsTableViewSource : UITableViewSource
    {
        private readonly NSString CELL_ID = new NSString("Weights Table Cell");
        private readonly string CellFormat = "{0} {1} x {2}";

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public List<KeyValuePair<string, int>> Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightBuddy.DataSources.WeightsTableViewSource"/> class.
        /// </summary>
        public WeightsTableViewSource()
        {
            Data = new List<KeyValuePair<string, int>>();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_ID);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, CELL_ID);
            }

            var values = Data[indexPath.Row];
            cell.TextLabel.Text = String.Format(CellFormat, values.Key, "lbs", values.Value); // TODO: make units come from settings

            return cell;
        }

        public override int RowsInSection(UITableView tableview, int section)
        {
            return Data.Count;
        }
    }
}

