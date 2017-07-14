using System;
using System.Collections.Generic;
using System.Drawing;
using Foundation;
using UIKit;

namespace ParentApp
{
	public class StudentList_tableviewsource: UITableViewSource
	{
		public static event EventHandler CellClicked = delegate { };
		List<StudentModel> tableItems;
		public StudentList_tableviewsource(List<StudentModel> items)
		{
			tableItems = items;

		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return tableItems.Count;
		}
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var type=StaticMethods.DeviceType();

			if (type == "ipad")
			{
				if (StaticDataModel.CurrentLanguage == "en")
				{
					///English
					var cell = (ipad_StudentList_tableviewcell)tableView.DequeueReusableCell(ipad_StudentList_tableviewcell.Key);
					if (cell == null)
					{
						cell = ipad_StudentList_tableviewcell.Create();
					}
					//cell.SelectionStyle = UITableViewCellSelectionStyle.None;//Default;
					var item = tableItems[indexPath.Row];
					var index = indexPath.Row;
					cell.UpdateCell(indexPath.Row, item.student_id, item.s_route_id, item.s_fname + " " + item.family_name, WebService.ImageUrl + item.s_image_path, item.blink_status, item.driver_name, item.driver_contact);
					cell.SelectionStyle = UITableViewCellSelectionStyle.None;

					if (index >= 0)
					{
						if (index % 2 == 0)
						{
							cell.BackgroundColor = new UIColor(red: 0.959f, green: 0.959f, blue: 0.959f, alpha: 1f);
						}
						else
						{
							cell.BackgroundColor = new UIColor(red: 0.999f, green: 1f, blue: 1f, alpha: 1f);

						}
					}
					cell.Frame = new RectangleF(0, 0, 139, 500);
					return cell;
				}
				else
				{ 
					///Arabic
				var cell = (ar_ipad_StudentList_tableviewcell)tableView.DequeueReusableCell(ar_ipad_StudentList_tableviewcell.Key);
					if (cell == null)
					{
						cell = ar_ipad_StudentList_tableviewcell.Create();
					}
					//cell.SelectionStyle = UITableViewCellSelectionStyle.None;//Default;
					var item = tableItems[indexPath.Row];
					var index = indexPath.Row;
					cell.UpdateCell(indexPath.Row, item.student_id, item.s_route_id, item.s_fname + " " + item.family_name, WebService.ImageUrl + item.s_image_path, item.blink_status, item.driver_name, item.driver_contact);
					cell.SelectionStyle = UITableViewCellSelectionStyle.None;

					if (index >= 0)
					{
						if (index % 2 == 0)
						{
							cell.BackgroundColor = new UIColor(red: 0.959f, green: 0.959f, blue: 0.959f, alpha: 1f);
						}
						else
						{
							cell.BackgroundColor = new UIColor(red: 0.999f, green: 1f, blue: 1f, alpha: 1f);

						}
					}
					cell.Frame = new RectangleF(0, 0, 139, 500);
					return cell;
				}
					
			}
			else
			{
				if (StaticDataModel.CurrentLanguage == "en")
				{
					///English
					var cell = (StudentList_tableviewcell)tableView.DequeueReusableCell(StudentList_tableviewcell.Key);
					if (cell == null)
					{
						cell = StudentList_tableviewcell.Create();
					}
					//cell.SelectionStyle = UITableViewCellSelectionStyle.None;//Default;
					var item = tableItems[indexPath.Row];
					var index = indexPath.Row;
					cell.UpdateCell(indexPath.Row, item.student_id, item.s_route_id, item.s_fname + " " + item.family_name, WebService.ImageUrl + item.s_image_path, item.blink_status, item.driver_name, item.driver_contact);
					cell.SelectionStyle = UITableViewCellSelectionStyle.None;

					if (index >= 0)
					{
						if (index % 2 == 0)
						{
							cell.BackgroundColor = new UIColor(red: 0.959f, green: 0.959f, blue: 0.959f, alpha: 1f);
						}
						else
						{
							cell.BackgroundColor = new UIColor(red: 0.999f, green: 1f, blue: 1f, alpha: 1f);

						}
					}
					return cell;
				}
				else
				{ 
				///Arabic
				var cell = (ar_StudentList_tableviewcell)tableView.DequeueReusableCell(ar_StudentList_tableviewcell.Key);
					if (cell == null)
					{
						cell = ar_StudentList_tableviewcell.Create(); 
					}
					//cell.SelectionStyle = UITableViewCellSelectionStyle.None;//Default;
					var item = tableItems[indexPath.Row];
					var index = indexPath.Row;
					cell.UpdateCell(indexPath.Row, item.student_id, item.s_route_id, item.s_fname + " " + item.family_name, WebService.ImageUrl + item.s_image_path, item.blink_status, item.driver_name, item.driver_contact);
					cell.SelectionStyle = UITableViewCellSelectionStyle.None;

					if (index >= 0)
					{
						if (index % 2 == 0)
						{
							cell.BackgroundColor = new UIColor(red: 0.959f, green: 0.959f, blue: 0.959f, alpha: 1f);
						}
						else
						{
							cell.BackgroundColor = new UIColor(red: 0.999f, green: 1f, blue: 1f, alpha: 1f);

						}
					}
					return cell;
				}
			
			}


		}


		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			//To deselect the selected row
			//tableView.DeselectRow(indexPath, true);

		}

		public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine("Row Deselect");
		}
	}
}
