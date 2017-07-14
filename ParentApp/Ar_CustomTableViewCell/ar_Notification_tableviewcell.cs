using System;

using Foundation;
using UIKit;

namespace ParentApp
{
	public partial class ar_Notification_tableviewcell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("ar_Notification_tableviewcell");
		public static readonly UINib Nib;

		static ar_Notification_tableviewcell()
		{
			Nib = UINib.FromName("ar_Notification_tableviewcell", NSBundle.MainBundle);
		}

		protected ar_Notification_tableviewcell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		public static ar_Notification_tableviewcell Create()
		{
			return (ar_Notification_tableviewcell)Nib.Instantiate(null, null)[0];
		}
		public void UpdateCell(string noti_id, string noti_type, string parent_id, string route_id, string msg, string date)
		{
			btnTitle.SetTitle(noti_type, UIControlState.Normal);
			lblDescrioption.Text = msg;
			btnDatetime.SetTitle(date, UIControlState.Normal);
		}
	}
}
