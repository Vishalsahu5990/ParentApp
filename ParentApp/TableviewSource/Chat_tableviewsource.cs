using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
namespace ParentApp
{
	public class Chat_tableviewsource : UITableViewSource
	{

		ChatModel model = null;
		public static event EventHandler<ChatModel> ViewDetailsClicked = delegate { };

		public Chat_tableviewsource()
		{

		}

		public static event EventHandler CellClicked = delegate { };
		List<ChatModel> tableItems;
		public Chat_tableviewsource(List<ChatModel> items)
		{
			tableItems = items;

		}
		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return tableItems.Count;
		}
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{

			var item = tableItems[indexPath.Row];
			var index = indexPath.Row;

			if (StaticDataModel.CurrentLanguage == "en")
			{
				//// For English
				if (item.sender != 1)
				{
					var cell = (Chat_tableviewcell)tableView.DequeueReusableCell(Chat_tableviewcell.Key);
					if (cell == null)
					{
						cell = Chat_tableviewcell.Create();
					}
					//cell.SelectionStyle = UITableViewCellSelectionStyle.None;//Default;

					cell.UpdateCell(item.sender, item
									.sender_id, item.status, item.reciever_id, item.time, item.msg, item.IsFromLocal);
					cell.SelectionStyle = UITableViewCellSelectionStyle.None;

					return cell;
				}
				else
				{
					var cell1 = (OutgoingMsgTableviewcell)tableView.DequeueReusableCell(OutgoingMsgTableviewcell.Key);
					if (cell1 == null)
					{
						cell1 = OutgoingMsgTableviewcell.Create();
					}
					//cell.SelectionStyle = UITableViewCellSelectionStyle.None;//Default;

					cell1.UpdateCell(item.sender, item
									.sender_id, item.status, item.reciever_id, item.time, item.msg, item.IsFromLocal);
					cell1.SelectionStyle = UITableViewCellSelectionStyle.None;

					return cell1;
				}
			}
			else
			{
				//// For Arabic
				if (item.sender != 1)
				{
					var cell = (ar_Chat_tableviewcell)tableView.DequeueReusableCell(ar_Chat_tableviewcell.Key);
					if (cell == null)
					{
						cell = ar_Chat_tableviewcell.Create();
					}
					//cell.SelectionStyle = UITableViewCellSelectionStyle.None;//Default;

					cell.UpdateCell(item.sender, item
									.sender_id, item.status, item.reciever_id, item.time, item.msg, item.IsFromLocal);
					cell.SelectionStyle = UITableViewCellSelectionStyle.None;

					return cell;
				}
				else
				{
					var cell1 = (ar_OutgoingMsgTableviewcell)tableView.DequeueReusableCell(ar_OutgoingMsgTableviewcell.Key);
					if (cell1 == null)
					{
						cell1 = ar_OutgoingMsgTableviewcell.Create();
					}
					//cell.SelectionStyle = UITableViewCellSelectionStyle.None;//Default;

					cell1.UpdateCell(item.sender, item
									.sender_id, item.status, item.reciever_id, item.time, item.msg, item.IsFromLocal);
					cell1.SelectionStyle = UITableViewCellSelectionStyle.None;

					return cell1;
				}
			}




		}


		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{


		}

		public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine("Row Deselect");
		}
	}
}
