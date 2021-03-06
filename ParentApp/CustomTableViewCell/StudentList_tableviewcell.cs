﻿using System;
using System.Threading.Tasks;
using Foundation;
using SDWebImage;
using UIKit;

namespace ParentApp
{
	public partial class StudentList_tableviewcell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("StudentList_tableviewcell");
		public static readonly UINib Nib;
		public static event EventHandler ViewDetailsClicked = delegate { };
		public static event EventHandler CarClicked = delegate { };

		static StudentList_tableviewcell()
		{
			Nib = UINib.FromName("StudentList_tableviewcell", NSBundle.MainBundle);
		}

		protected StudentList_tableviewcell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		public static StudentList_tableviewcell Create()
		{
			return (StudentList_tableviewcell)Nib.Instantiate(null, null)[0];
		}
		public void UpdateCell(int index,string student_id,string s_route_id,string student_name,string profilePic,string blink_status, string drivername, string drivercontact)
		{
			try
			{
				if (StaticDataModel.CurrentLanguage == "ar")
				{
					btnViewDetails.SetTitle("عرض التفاصيل", UIControlState.Normal);
					lbldriver.Text = ": سائق";
				}

				StaticDataModel.StudentRouteId = Convert.ToInt32(s_route_id);
				var blinkStatus = Convert.ToInt32(blink_status);
				if (blinkStatus == 1)
				{
					StartBlinkingCarImage();

				
				}
				imgProfile.Layer.BorderWidth = 2;
				imgProfile.Layer.BorderColor = UIColor.DarkGray.CGColor;
				lblStudentName.Text = student_name;
				lblDriverName.Text = drivername;

				imgProfile.SetImage(
						url: new NSUrl(profilePic),
											placeholder: UIImage.FromBundle("dummy_StudentProile.png")
											);
				btnCar.TouchUpInside += (object sender, EventArgs e) =>
				 {
					StaticDataModel.StudentId = Convert.ToInt32(student_id);
					 StaticDataModel.StudentRouteId = Convert.ToInt32(s_route_id);
					 StaticDataModel.BlinkingStatus = Convert.ToInt32(blink_status);
					 StaticDataModel.StudentProfilePic = profilePic;
					 StaticDataModel.StudentInfo = student_name;
					CarClicked(this, EventArgs.Empty);

				 };
				btnViewDetails.TouchUpInside += (object sender, EventArgs e) =>
				 {
					StaticDataModel.StudentId = Convert.ToInt32(student_id);
					 StaticDataModel.StudentRouteId = Convert.ToInt32(s_route_id);
					 StaticDataModel.BlinkingStatus = Convert.ToInt32(blink_status);
					StaticDataModel.StudentProfilePic = profilePic;
					StaticDataModel.StudentInfo = student_name;

					ViewDetailsClicked(this, EventArgs.Empty);

				 };
				btnCall.TouchUpInside += (object sender, EventArgs e) =>
				 { 
					if (drivercontact != string.Empty)
					 {
						 drivercontact = drivercontact.Replace("\"", string.Empty).Trim();
						 var url = new NSUrl("tel:" + drivercontact);
						 if (!UIApplication.SharedApplication.OpenUrl(url))
						 {
							 var av = new UIAlertView("Not supported",
							   "Scheme 'tel:' is not supported on this device",
							   null,
							   "OK",
							   null);
							 av.Show();
						 };

					 }
				 };
			}
			catch (Exception ex)
			{

			}

		}
		private async void StartBlinkingCarImage()
		{
			
			UIView.Animate(1, 2, UIViewAnimationOptions.Autoreverse | UIViewAnimationOptions.Repeat, () =>
	{
		imgCar.Alpha = 0;
		
	}, null);
		}
	}
}
