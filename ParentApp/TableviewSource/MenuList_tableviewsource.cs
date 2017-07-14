using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace ParentApp
{
	public partial class MenuList_tableviewsource : UITableViewSource
	{
		public static event EventHandler CellClicked = delegate { };
		int[] tableItems;
		public MenuList_tableviewsource(int[] items)
		{
			tableItems = items; 

		}
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return 9;
		}
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (StaticDataModel.CurrentLanguage == "en")
			{
				///English
				var cell = (MenuList_tableviewcell)tableView.DequeueReusableCell(MenuList_tableviewcell.Key);
				if (cell == null)
				{
					cell = MenuList_tableviewcell.Create(); 
				}
				//cell.SelectionStyle = UITableViewCellSelectionStyle.None;//Default;
				var item = tableItems[indexPath.Row]; 
				cell.UpdateCell(item);
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
				cell.BackgroundColor = UIColor.Clear;
				return cell;
			}
			else
			{ 
			///arabic

				var cell = (ar_MenuList_tableviewcell)tableView.DequeueReusableCell(ar_MenuList_tableviewcell.Key1);
				if (cell == null)
				{
					
					cell = ar_MenuList_tableviewcell.Create();
				}
				//cell.SelectionStyle = UITableViewCellSelectionStyle.None;//Default;
				var item = tableItems[indexPath.Row];
				cell.UpdateCell(item);
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
				cell.BackgroundColor = UIColor.Clear;
				return cell;
			}

		 }


		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			//To deselect the selected row
         //tableView.DeselectRow(indexPath, true);
			var item = tableItems[indexPath.Row];
			StaticDataModel.CurrentMenuItemPosition = item;
			CellClicked(this, EventArgs.Empty);
		}

		public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine("Row Deselect");
		}

	}
}
