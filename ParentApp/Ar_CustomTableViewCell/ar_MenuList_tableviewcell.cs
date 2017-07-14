using System;

using Foundation;
using UIKit;

namespace ParentApp
{
	public partial class ar_MenuList_tableviewcell : UITableViewCell
	{
		public static readonly NSString Key1 = new NSString("ar_MenuList_tableviewcell");
		public static readonly UINib Nib1;

	static ar_MenuList_tableviewcell()
		{
			Nib1 = UINib.FromName("ar_MenuList_tableviewcell", NSBundle.MainBundle);
		}
		public static ar_MenuList_tableviewcell Create() 
		{
			return (ar_MenuList_tableviewcell)Nib1.Instantiate(null, null)[0];
		}
		protected ar_MenuList_tableviewcell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		public void UpdateCell(int position)
		{

			Menuitemselector(position);
			lblText.TextColor = UIColor.White;

		}
		private void Menuitemselector(int position) 
		{
			switch (position)
			{
				case 1:
					if (StaticDataModel.CurrentLanguage == "ar")
					{
						lblText.Text = "الصفحة الرئيسية";
					}
					else
					{ 
					lblText.Text = "Home";
					}

					imgIcon.Image = UIImage.FromFile("home.png");
					break;

				case 2:
					if (StaticDataModel.CurrentLanguage == "ar")
					{
						lblText.Text = "الملف الشخصي";
					}
					else
					{
						lblText.Text = "Profile";
					}

					imgIcon.Image = UIImage.FromFile("profile.png");
					break;

				case 3:
					if (StaticDataModel.CurrentLanguage == "ar")
					{
						lblText.Text = "مراسلة المدرسة";
					}
					else
					{
						lblText.Text = "Chat with School";
					}

					imgIcon.Image = UIImage.FromFile("chat.png");
					break;

				case 4:
					if (StaticDataModel.CurrentLanguage == "ar")
					{
						lblText.Text = "الإشعارات";
					}
					else
					{
						lblText.Text = "Notifications";
					}
				
					imgIcon.Image = UIImage.FromFile("notification.png");
					break;

				case 5:
					if (StaticDataModel.CurrentLanguage == "ar")
					{
						lblText.Text = "الإبلاغ عن غياب ";
					}
					else
					{
						lblText.Text = "Report Absent";
					}
                   
					imgIcon.Image = UIImage.FromFile("report-absent.png");
					break;

				case 6:
					if (StaticDataModel.CurrentLanguage == "ar")
					{
						lblText.Text = "تغيير كلمة المرور";
					}
					else
					{
						lblText.Text = "Change Password";
					}

					imgIcon.Image = UIImage.FromFile("change-pw.png");
					break;
				case 7:
					if (StaticDataModel.CurrentLanguage == "ar")
					{
						lblText.Text = "تغيير اللغة";
					}
					else
					{
						lblText.Text = "Change Language";
					}

					imgIcon.Image = UIImage.FromFile("language.png");
					break;

				case 8:
					if (StaticDataModel.CurrentLanguage == "ar")
					{
						lblText.Text = "الإعدادات";
					}
					else
					{
						lblText.Text = "Settings";
					}

					imgIcon.Image = UIImage.FromFile("setting.png");
					break;

				case 9:
					if (StaticDataModel.CurrentLanguage == "ar")
					{
						lblText.Text = "خروج";
					}
					else
					{
						lblText.Text = "Logout";
					}

					imgIcon.Image = UIImage.FromFile("logout.png");
					break;
			}

		}
	}
}
