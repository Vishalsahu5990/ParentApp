using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace ParentApp
{
	public class Notification_tableviewsource: UITableViewSource
	{
		DropdownModel model = null;
		public static event EventHandler<DropdownModel> ViewDetailsClicked = delegate { };
		public Notification_tableviewsource()
		{
		}

		public static event EventHandler CellClicked = delegate { };
		List<NotificationModel> tableItems;
		public Notification_tableviewsource(List<NotificationModel> items)
		{
			tableItems = items;

		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return tableItems.Count;
		}
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (StaticDataModel.CurrentLanguage == "en")
			{
				///English
				var cell = (Notification_tableviewcell)tableView.DequeueReusableCell(Notification_tableviewcell.Key);
				if (cell == null)
				{
					cell = Notification_tableviewcell.Create();
				}
				//cell.SelectionStyle = UITableViewCellSelectionStyle.None;//Default;
				var item = tableItems[indexPath.Row];
				var index = indexPath.Row;
				cell.UpdateCell(item.noti_id, item.noti_type, item
								.parent_id, item.route_id, item.msg, item.date);
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;

				return cell;
			}
			else
			{ 
				///arabic
			var cell = (ar_Notification_tableviewcell)tableView.DequeueReusableCell(ar_Notification_tableviewcell.Key);
				if (cell == null)
				{
					cell = ar_Notification_tableviewcell.Create();
				}
				//cell.SelectionStyle = UITableViewCellSelectionStyle.None;//Default;
				var item = tableItems[indexPath.Row];
				var index = indexPath.Row;
				cell.UpdateCell(item.noti_id, item.noti_type, item
								.parent_id, item.route_id, item.msg, item.date);
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;

				return cell;
			}

		}


		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			model = new DropdownModel();
			var item = tableItems[indexPath.Row];
			model.Code = item.noti_type;
			model.Name = item.msg;
			ViewDetailsClicked(this, model);

			//To deselect the selected row
			//tableView.DeselectRow(indexPath, true);

		}

		public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine("Row Deselect");
		}
	}
}
