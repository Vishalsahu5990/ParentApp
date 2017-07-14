using System;

using Foundation;
using UIKit;

namespace ParentApp
{
	public partial class ar_Chat_tableviewcell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("ar_Chat_tableviewcell");
		public static readonly UINib Nib;

		static ar_Chat_tableviewcell()
		{
			Nib = UINib.FromName("ar_Chat_tableviewcell", NSBundle.MainBundle);
		}

		protected ar_Chat_tableviewcell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		public static ar_Chat_tableviewcell Create()
		{
			return (ar_Chat_tableviewcell)Nib.Instantiate(null, null)[0];
		}
		public void UpdateCell(int sender, int sender_id, int status, int reciever_id, string time, string msg, bool isFromLocal)
		{
			var t = Convert.ToDateTime(time);
			if (!isFromLocal)
			{
				t = DateTime.SpecifyKind(t, DateTimeKind.Utc);
				t = t.ToLocalTime();
			}
			lbl1.Text = t.ToString("yyyy-MM-dd hh:mm:ss tt");
			lbl2.Text = msg;
			lbl3.Text = StaticDataModel.SchoolAdminName;
		}

	}
}

