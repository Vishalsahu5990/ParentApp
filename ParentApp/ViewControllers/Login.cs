using Foundation;
using System;
using UIKit;
using BigTed;
using System.Threading.Tasks;
using System.Json;

namespace ParentApp
{
    public partial class Login : UIViewController
    {
		

		public static NSString key = new NSString("dict");
		public static NSDictionary dictionary = null;
		public static NSString key1 = new NSString("dict1");
		public static NSDictionary dictionary1 = null;
		public static NSString key2 = new NSString("dict2");
		public static NSDictionary dictionary2 = null;

		public static NSString UserId = new NSString("UserId");
		public static NSString UserName = new NSString("UserName");
		public static NSString ProfilePic = new NSString("ProfilePic");
		public static NSString Password = new NSString("Password");
		public static NSString SchoolName = new NSString("SchoolName");
public static NSString school_admin_name = new NSString("school_admin_name");

		public static NSString user_email = new NSString("user_email");
		public static NSString first_name = new NSString("first_name");
		public static NSString middle_name = new NSString("middle_name");
		public static NSString family_name = new NSString("family_name");
		public static NSString contact_number = new NSString("contact_number");
		public static NSString mobile_number = new NSString("mobile_number");
		public static NSString IsRememberedUsernamePwd = new NSString("IsRememberedUsernamePwd");
		public static NSString sms_evening_before = new NSString("sms_evening_before");
		public static NSString max_speed = new NSString("max_speed");
		public static NSString evening_before = new NSString("evening_before");
		public static NSString lang = new NSString("lang");
		public static NSString noti_on = new NSString("noti_on");
		public static NSString checked_out_on = new NSString("checked_out_on");
		public static NSString speed_on = new NSString("speed_on");
		public static NSString morning_before = new NSString("morning_before");
		public static NSString role = new NSString("role");
		public static NSString instant_message = new NSString("instant_message");
		public static NSString school_admin_number = new NSString("school_admin_number");
		public static NSString sms_checked_out_on = new NSString("sms_checked_out_on");
		public static NSString school_admin = new NSString("school_admin");
		public static NSString sms_speed_on = new NSString("sms_speed_on");
		public static NSString sms_morning_before = new NSString("sms_morning_before");
		public static NSString chat_on = new NSString("chat_on");
		public static NSString wrong_route_on = new NSString("wrong_route_on");
		public static NSString sms_wrong_route_on = new NSString("sms_wrong_route_on");
		public static NSString sms_instant_message = new NSString("sms_instant_message");
		public static NSString sms_max_speed = new NSString("sms_max_speed");
		public static NSString sms_checked_in_on = new NSString("sms_checked_in_on");
		public static NSString checked_in_on = new NSString("checked_in_on");
		public static NSString IsFirstTime = new NSString("IsFirstTime");

		private int count = 0;
		public  bool RememberPassword = false;
		public UserModel usermodel = null;
		public string ContactNo = string.Empty;
		public bool IsfromTextfield = false;
        public Login (IntPtr handle) : base (handle)
        {
        }
		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();


			UIKeyboard.Notifications.ObserveWillShow((sender, e) => 
			{
				if (uiviewUPlogin.Hidden)
				{
					uiviewUPlogin.Hidden = false;
					if (IsfromTextfield)
						txtPassword_up.BecomeFirstResponder();
					else
						txtLogin_up.BecomeFirstResponder();
				}
			});
			UIKeyboard.Notifications.ObserveWillHide((sender, e) =>
			{

				if (!uiviewUPlogin.Hidden)
				{
					txtUserid.Text = txtLogin_up.Text;
					txtPassword.Text = txtPassword_up.Text;
					uiviewUPlogin.Hidden = true;
				}
			});
			 
			txtUserid.ShouldReturn += (textField) => 
			{
				
					textField.ResignFirstResponder();
				return true;
			};
			txtPassword.ShouldReturn += (textField) =>
			{
				textField.ResignFirstResponder();
				return true;
			};
			txtPassword.EditingDidBegin += (sender, e) =>
			 {
				 IsfromTextfield = true;
			 };
			txtUserid.EditingDidBegin += (sender, e) =>
			 {
				IsfromTextfield = false;
			 };

			PrepareUI();
			GetContactNo();
			uiviewCall.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
		   {
				if(ContactNo!=string.Empty)
				{
					ContactNo = ContactNo.Replace("\"", string.Empty).Trim();
					UIApplication.SharedApplication.OpenUrl(NSUrl.FromString("tel://" + ContactNo));
				
				//var url = new NSUrl("tel:" + ContactNo);
				//if (!UIApplication.SharedApplication.OpenUrl(url))
				//   {
				//	   var av = new UIAlertView("Not supported",
				//		 "Scheme 'tel:' is not supported on this device",
				//		 null,
				//		 "OK",
				//		 null);
				//	   av.Show();
				//   };

			    }
				else
				{
				BTProgressHUD.ShowToast("Unable to fetch contact details.", false, 1000);	
				};
		   }));

		}

		private void PrepareUI()
		{
			try
			{
				string uid = string.Empty;
				string upd = string.Empty;
				if (StaticDataModel.CurrentLanguage == "en")
				{
					uid = "User Id";
					upd = "Password";
				}
				else
				{ 
				    uid = "اسم المستخدم";
					upd = "كلمة المرور";
				}
				txtUserid.AttributedPlaceholder = new NSAttributedString(uid, null, UIColor.White);
				txtPassword.AttributedPlaceholder = new NSAttributedString(upd, null, UIColor.White);

				txtLogin_up.AttributedPlaceholder = new NSAttributedString(uid, null, UIColor.White);
				txtPassword_up.AttributedPlaceholder = new NSAttributedString(upd, null, UIColor.White);

				
				btnCheckBox.Layer.BorderWidth = 2;
				btnCheckBox.Layer.BorderColor = UIColor.Black.CGColor;


				btnCheckBoxUp.Layer.BorderWidth = 2;
				btnCheckBoxUp.Layer.BorderColor = UIColor.Black.CGColor;

				//Code to remember username and password
			Login.dictionary1 = NSUserDefaults.StandardUserDefaults.DictionaryForKey(Login.key1);
			string isRemember = Login.dictionary1["IsRememberedUsernamePwd"].ToString();
			string userid = Login.dictionary1["UserName"].ToString();
			string password = Login.dictionary1["Password"].ToString();

				if (isRemember=="1")
				{
					btnCheckBoxUp.SetBackgroundImage(UIImage.FromFile("checked.png"), UIControlState.Normal);

					btnCheckBox.SetBackgroundImage(UIImage.FromFile("checked.png"), UIControlState.Normal);
					RememberPassword = true;
					count = 1;
					txtUserid.Text =userid;
					txtPassword.Text =password;

					btnCheckBox.Layer.BorderWidth = 0;
					btnCheckBoxUp.Layer.BorderWidth = 0;

				}

			
			}
			catch (Exception ex)
			{
				
			}
			SetFontSize();
		}
		private void SetFontSize()
		{
			if (StaticDataModel.DeviceHeight > 800)
			{
				btnRememberMe.Font = UIFont.SystemFontOfSize(19);


				btnParentLogin.Font = UIFont.SystemFontOfSize(27);
				btnParentLoginup.Font = UIFont.SystemFontOfSize(27);

				btnForgotPassword.Font = UIFont.SystemFontOfSize(19);
				btnCallLabel.Font = UIFont.SystemFontOfSize(19);

				btnLoginMain.Font = UIFont.SystemFontOfSize(27);




			}
		}

        partial void BtnLoginMain_TouchUpInside(UIButton sender)
		{
			LoginClicked();
		}

		partial void BtnForgotPassword_TouchUpInside(UIButton sender)
		{
			ForgotPassword forgotpassword = this.Storyboard.InstantiateViewController("ForgotPassword") as ForgotPassword;
			if (forgotpassword != null)
			{
				this.PresentViewController(forgotpassword, true, null);
			}
		}


        partial void BtnLogin_TouchUpInside(UIButton sender)
		{
			txtUserid.Text=txtLogin_up.Text;
			txtPassword.Text = txtPassword_up.Text;
			LoginClicked();
			
		}
		private void LoginClicked()
		{ 
		try
			{
				//txtUserid.Text = txtLogin_up.Text;
				//txtPassword.Text = txtPassword_up.Text;

				if (!StaticMethods.IsConnectedToInternet())
				{
					BTProgressHUD.ShowToast("You are not connected to internet.", false, 1000);

				}
				else
				{

					if (IsValidate())
					{
						if (txtLogin_up.Text != string.Empty)
						{
							txtUserid.Text = txtLogin_up.Text;
							txtPassword.Text = txtPassword_up.Text;
						}
						LoginProcess(txtUserid.Text,txtPassword.Text); ;
					}

				}
			}
			catch (Exception ex)
			{

			}
		}

        partial void BtnRememberMeUp_TouchUpInside(UIButton sender)
		{
			CheckBoxOnOff();
		}

        partial void BtnCheckBoxUp_TouchUpInside(UIButton sender)
		{
			CheckBoxOnOff();
		}

		partial void BtnCheckBox_TouchUpInside(UIButton sender)
		{
			CheckBoxOnOff();
		}

        partial void BtnRememberMe_TouchUpInside(UIButton sender)
		{
			CheckBoxOnOff();  
		}
		private void CheckBoxOnOff()
		{ 
			try
			{	 if (count % 2 == 0)
				{
					btnCheckBoxUp.SetBackgroundImage(UIImage.FromFile("checked.png"), UIControlState.Normal);
					btnCheckBox.SetBackgroundImage(UIImage.FromFile("checked.png"), UIControlState.Normal);

					btnCheckBox.Layer.BorderWidth = 0;
					btnCheckBoxUp.Layer.BorderWidth = 0;
					RememberPassword = true;
				}
				else
				{
				    btnCheckBox.Layer.BorderWidth = 2;
					btnCheckBoxUp.Layer.BorderWidth = 2;
					btnCheckBoxUp.SetBackgroundImage(UIImage.FromFile("uncheck.png"), UIControlState.Normal);
					btnCheckBox.SetBackgroundImage(UIImage.FromFile("uncheck.png"), UIControlState.Normal);

					RememberPassword = false;
				}
				count++;
    
			}
			catch (Exception ex)
			{

			}
		}
		private bool IsValidate()
		{
			if (txtUserid.Text == string.Empty)
			{
				BTProgressHUD.ShowToast("Username required.", false, 1000);
				return false;

			}
			else if (txtPassword.Text == string.Empty)
			{

				BTProgressHUD.ShowToast("Password required.", false, 1000);
				return false;

			}
			else
			{
				return true;
			}
		}
		private async void GetContactNo()
		{
			
			try
			{
				BTProgressHUD.Show();
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
					ContactNo=	WebService.GetContactNo();
					}).ContinueWith(
					t =>
					{
					if (ContactNo!=string.Empty)
						{
							
						}
						else
						{
							BTProgressHUD.ShowToast("Unable to fetch contact details.", false, 1000);
						}
						BTProgressHUD.Dismiss();


					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
			}
			catch (Exception ex)
			{

			}
			finally
			{

			}
		}
		private async void LoginProcess(string username,string password)
		{
			usermodel = new UserModel();
			try
			{
				BTProgressHUD.Show();
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
					usermodel =  WebService.Login(username,password);
					}).ContinueWith(
					t =>
					{
					if (usermodel!=null)
						{
							if (usermodel.result != "failed")
							{
								StaticDataModel.ProfilePic = usermodel.school_logo;

								StaticDataModel.SchoolName = usermodel.school_name.Replace("\"", string.Empty).Trim();
								usermodel.result = usermodel.result.Replace("\"", string.Empty).Trim();
								usermodel.responseMessage = usermodel.responseMessage.Replace("\"", string.Empty).Trim();

								if (usermodel.result != "failed")
								{
									var id = usermodel.user_id;
									usermodel.user_id = id.Replace("\"", string.Empty).Trim();
									SaveUserPrefrences();
									//static data holder
									var ids = usermodel.user_id;
									StaticDataModel.UserId = Convert.ToInt32(ids.Replace("\"", string.Empty).Trim());
									Home home = this.Storyboard.InstantiateViewController("Home") as Home;
									//if (home != null)
									//{
									//	this.PresentViewController(home, true, null);

									//}
									UIApplication.SharedApplication.KeyWindow.RootViewController = home;
							}
						}
							else
							{ 
							BTProgressHUD.ShowToast(usermodel.responseMessage.Replace("\"", string.Empty).Trim(), false, 10000);
						    }
					}
							else
							{
								BTProgressHUD.ShowToast("Incorrect user id or password, Please try again later.", false, 10000);
							}
						BTProgressHUD.Dismiss();


					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
			}
			catch (Exception ex)
			{

			}
			finally
			{
				
			}
		}
		private void SaveUserPrefrences()
		{ 
		try
			{
				//Is first time remember
				dictionary2 = new NSDictionary(IsFirstTime,1);
				NSUserDefaults.StandardUserDefaults.SetValueForKey(dictionary2, key2);

				//To save the AutoLogin Details
				dictionary = new NSDictionary(UserName, txtUserid.Text,
				                              UserId, usermodel.user_id,
				                              ProfilePic, usermodel.school_logo,
				                              Password, txtPassword.Text,
				                              IsRememberedUsernamePwd, RememberPassword,
				                              SchoolName, StaticDataModel.SchoolName,
				                              school_admin_name, usermodel.school_admin_name,
				                              user_email, usermodel.user_email,
				                              first_name, usermodel.first_name,
				                              middle_name, usermodel.middle_name,
				                              family_name, usermodel.family_name,
				                              contact_number, usermodel.contact_number,
				                              mobile_number, usermodel.mobile_number,
				                             sms_evening_before, usermodel.sms_evening_before,
				                             max_speed, usermodel.max_speed,
				                             evening_before, usermodel.evening_before,
				                             lang, usermodel.lang,
				                             noti_on, usermodel.noti_on,
				                             checked_out_on, usermodel.checked_out_on,
				                             speed_on, usermodel.speed_on,
				                             morning_before, usermodel.morning_before,
				                             role, usermodel.role,
				                             instant_message, usermodel.instant_message,
				                             school_admin_number, usermodel.school_admin_number,
				                             sms_checked_out_on, usermodel.sms_checked_out_on,
				                             school_admin, usermodel.school_admin,
				                             sms_speed_on, usermodel.sms_speed_on,
				                             sms_morning_before, usermodel.sms_morning_before,
				                             chat_on, usermodel.chat_on,
				                             wrong_route_on, usermodel.wrong_route_on,
				                             sms_wrong_route_on, usermodel.sms_wrong_route_on,
				                             sms_instant_message, usermodel.sms_instant_message,
				                             sms_max_speed, usermodel.sms_max_speed,
				                             sms_checked_in_on, usermodel.sms_checked_in_on,
				                             checked_in_on, usermodel.checked_in_on);
					

		NSUserDefaults.StandardUserDefaults.SetValueForKey(dictionary, key);
				//Remember me
				dictionary1 = new NSDictionary(UserName, txtUserid.Text,
				                               Password, txtPassword.Text,
				                               IsRememberedUsernamePwd, RememberPassword);
				NSUserDefaults.StandardUserDefaults.SetValueForKey(dictionary1, key1);
			}
			catch (Exception ex)
			{

			}
		}
	}
}