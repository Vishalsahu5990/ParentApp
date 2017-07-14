using System;

using Foundation;
using UIKit;

namespace ParentApp
{
	public partial class OutgoingMsgTableviewcell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("OutgoingMsgTableviewcell");
		public static readonly UINib Nib;

		static OutgoingMsgTableviewcell()
		{
			Nib = UINib.FromName("OutgoingMsgTableviewcell", NSBundle.MainBundle);
		}

		protected OutgoingMsgTableviewcell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		public static OutgoingMsgTableviewcell Create()
		{
			return (OutgoingMsgTableviewcell)Nib.Instantiate(null, null)[0];
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
