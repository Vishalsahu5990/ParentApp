using System;

using Foundation;
using UIKit;

namespace ParentApp
{
	public partial class ar_OutgoingMsgTableviewcell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("ar_OutgoingMsgTableviewcell");
		public static readonly UINib Nib;

		static ar_OutgoingMsgTableviewcell()
		{
			Nib = UINib.FromName("ar_OutgoingMsgTableviewcell", NSBundle.MainBundle);
		}

		protected ar_OutgoingMsgTableviewcell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		public static ar_OutgoingMsgTableviewcell Create()
		{
			return (ar_OutgoingMsgTableviewcell)Nib.Instantiate(null, null)[0];
		}
		public void UpdateCell(int sender, int sender_id, int status, int reciever_id, string time, string msg, bool isFromLocal)
		{
			try
			{
				if (status == 1)
				{
					btnTickMark.SetImage(UIImage.FromFile("seen.png"), UIControlState.Normal);
				}
				var t = Convert.ToDateTime(time);
				if (!isFromLocal)
				{
					t = DateTime.SpecifyKind(t, DateTimeKind.Utc);
					t = t.ToLocalTime();
				}
				lbl1.Text = t.ToString("yyyy-MM-dd hh:mm:ss tt");
				lbl2.Text = msg;


			}
			catch (Exception ex)
			{

			}


		}
	}
}
