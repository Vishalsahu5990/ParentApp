using Foundation;
using System;
using UIKit;
using SidebarNavigation;
using SDWebImage;
using BigTed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParentApp
{
	public partial class StudentList : BaseController
    {
		string sms_evening_before = string.Empty;
		string max_speed = string.Empty;
		string evening_before = string.Empty;
		string lang = string.Empty;
		string noti_on = string.Empty;
		string checked_out_on = string.Empty;
		string speed_on = string.Empty;
		string morning_before = string.Empty;
		string role = string.Empty;
		string instant_message = string.Empty;
		string school_admin_number = string.Empty;
		string sms_checked_out_on = string.Empty;
		string school_admin = string.Empty;
		string sms_speed_on = string.Empty;
		string sms_morning_before = string.Empty;
		string chat_on = string.Empty;
		string wrong_route_on = string.Empty;
		string sms_wrong_route_on = string.Empty;
		string sms_instant_message = string.Empty;
		string sms_max_speed = string.Empty;
		string sms_checked_in_on = string.Empty;
		string checked_in_on = string.Empty;

		public List<StudentModel> model = null;
		LanguagePopup cpuv;
		public StudentList (IntPtr handle) : base (handle)
        {
			
        }
		public StudentList() : base(null, null)
		{
			
		
		}

      
		partial void Btn_TouchUpInside(UIButton sender)
		{
			//InvokeOnMainThread(() =>
			//	{

			//		StaticMethods.ChangeLocalization("en");
			//		StaticDataModel.CurrentLanguage = "en";
			//	SidebarController.MenuLocation = SidebarNavigation.SidebarController.MenuLocations.Left;

			//	});

		}


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			InvokeOnMainThread(() =>
				{
				    
					var type=StaticMethods.DeviceType();
				    if(type!="ipad")
					tblStudentList.RowHeight = 94;
				
										
				});
			//if(!StaticDataModel.IsFromLanguageMenu)
			NavigationController.SetNavigationBarHidden(true, true);

						//GetStudentList();	

			SetData();

			imgMenu.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
		   {
			  SidebarController.OpenMenu();

		   }))	;
			StudentList_tableviewcell.CarClicked += (object sender, EventArgs e) =>
			 {
				 if (StaticDataModel.isEnableStudentTracking)
				 {
					 StudentTracking home = this.Storyboard.InstantiateViewController("StudentTracking") as StudentTracking;
					 if (home != null)
					 {
						 this.PresentModalViewController(home, true);
					 }
					 else
					 {
						
					 }
				 }
				 else
				 {
					 string message = string.Empty;
					 if (StaticDataModel.CurrentLanguage == "en")
					 {
						 message = "Your child is not in bus.";
					 }
					 else
					 { 
					message = ".طفلك ليس في الحافلة";
					}

					BTProgressHUD.ShowToast(message, false, 10500);

				 }
			 };
			StudentList_tableviewcell.ViewDetailsClicked += (object sender, EventArgs e) =>
			 {
				StudentReport sr = this.Storyboard.InstantiateViewController("StudentReport") as StudentReport;
				 if (sr != null)
				 {
					 this.PresentModalViewController(sr, true);

				 }
			 };

				ar_StudentList_tableviewcell.CarClicked += (object sender, EventArgs e) =>
		 {
			 StudentTracking home = this.Storyboard.InstantiateViewController("StudentTracking") as StudentTracking;
			 if (home != null)
			 {
				 this.PresentModalViewController(home, true);

			 }
		 };
			ar_StudentList_tableviewcell.ViewDetailsClicked += (object sender, EventArgs e) =>
			 {
				 StudentReport sr = this.Storyboard.InstantiateViewController("StudentReport") as StudentReport;
				 if (sr != null)
				 {
					 this.PresentModalViewController(sr, true);

				 }
			 };
			ipad_StudentList_tableviewcell.CarClicked += (object sender, EventArgs e) =>
			 {
				 StudentTracking home = this.Storyboard.InstantiateViewController("StudentTracking") as StudentTracking;
				 if (home != null)
				 {
					 this.PresentModalViewController(home, true);

				 }
			 };
			ipad_StudentList_tableviewcell.ViewDetailsClicked += (object sender, EventArgs e) =>
			 {
				 StudentReport sr = this.Storyboard.InstantiateViewController("StudentReport") as StudentReport;
				 if (sr != null)
				 {
					 this.PresentModalViewController(sr, true);

				 }
			 };
			ar_ipad_StudentList_tableviewcell.CarClicked += (object sender, EventArgs e) =>
			 {
				 StudentTracking home = this.Storyboard.InstantiateViewController("StudentTracking") as StudentTracking;
				 if (home != null)
				 {
					 this.PresentModalViewController(home, true);

				 }
			 };
			ar_ipad_StudentList_tableviewcell.ViewDetailsClicked += (object sender, EventArgs e) =>
			 {
				 StudentReport sr = this.Storyboard.InstantiateViewController("StudentReport") as StudentReport;
				 if (sr != null)
				 {
					 this.PresentModalViewController(sr, true);

				 }
			 };

			AppDelegate.NotificationReceived += (object sender, EventArgs e) =>

			 { 
            GetStudentList();
			 };

Settings.SaveSettings += (object sender, EventArgs e) =>
 {
	 ChangeLaguageProcess();
			};
		}
		private void ChangeLaguageProcess()
		{
			try
			{
				AppDelegate ap = new AppDelegate();
				if (StaticDataModel.CurrentLanguage == "en")
				{

					Home home = ap.MainStoryboard.InstantiateViewController("Home") as Home;
					UIApplication.SharedApplication.KeyWindow.RootViewController = home;
					//StaticMethods.ChangeLocalization("en");
				}
				else
				{
					Home home = ap.Main_ArabicStoryboard.InstantiateViewController("Home") as Home;
					UIApplication.SharedApplication.KeyWindow.RootViewController = home;
					//StaticMethods.ChangeLocalization("ar"); 
				}
				PrepareData();
			}
			catch (Exception ex)
			{


			}
		}
		public async override void ViewDidLayoutSubviews()
		{
			
			base.ViewDidLayoutSubviews(); 

		}

		void Cpuv_PopWillClose() 
		{
			StaticDataModel.IsFromLanguageMenu = false;
			Console.WriteLine("cpuv will close");
			AppDelegate ap = new AppDelegate();
			if (StaticDataModel.CurrentLanguage == "en")
			{
				
				Home home = ap.MainStoryboard.InstantiateViewController("Home") as Home;
				UIApplication.SharedApplication.KeyWindow.RootViewController = home;
				//StaticMethods.ChangeLocalization("en");
			}
			else
			{
				Home home = ap.Main_ArabicStoryboard.InstantiateViewController("Home") as Home;
				UIApplication.SharedApplication.KeyWindow.RootViewController = home;
				//StaticMethods.ChangeLocalization("ar"); 
			}
			PrepareData();

		}

		public override void ViewDidDisappear(bool animated)
		{
			
			base.ViewDidDisappear(animated);
			model = null;

		}
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			if (StaticDataModel.IsFromLanguageMenu)
			{
				//uiview.Hidden = false;
				cpuv = new LanguagePopup(new CoreGraphics.CGSize(300, 200), uiview,uiviewLanguageBorder,btnArabic,btnEnglish,btnSave,btnCancel);
				cpuv.PopWillClose += Cpuv_PopWillClose;
				cpuv.PopUp(true, delegate
				   {
					   Console.WriteLine("cpuv will close");
					   this.DismissViewController(false, null);
				   });
			}
			GetStudentList();
		}
		private void SetData()
		{
			try
			{
				
				if (StaticDataModel.ProfilePic != string.Empty)
				{
					lblSchoolName.SetTitle(StaticDataModel.SchoolName, UIControlState.Normal);
					StaticDataModel.ProfilePic = StaticDataModel.ProfilePic.Replace("\"", string.Empty).Trim();
					StaticDataModel.ProfilePic = StaticDataModel.ProfilePic.Replace(" ", "%20").Trim();

					var url = WebService.SchoolImageUrl + StaticDataModel.ProfilePic;
					Console.WriteLine(url);

					imgSchoolLogo.SetImage(
						url: new NSUrl(url),
											placeholder: UIImage.FromBundle("dummmyprofile_withBorder.png")
										  );
				}
			}
			catch (Exception ex)
			{

			}
		}
		static UIImage FromUrl(string uri)
		{
			using (var url = new NSUrl(uri))
			using (var data = NSData.FromUrl(url))
				return UIImage.LoadFromData(data);
		}
		private void GetStudentList()
		{
			tblStudentList.Source = null;
List<StudentModel> 	model1= null;
			try
			{
				BTProgressHUD.Show("Fetching List...");
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						
					model1= new List<StudentModel>();
					model1 = WebService.GetStudentByParentId();
				}
				///
				).ContinueWith(
					t =>
					{

						if (model1 != null)
						{
							tblStudentList.Source = null;
						    tblStudentList.Source = new StudentList_tableviewsource(model1);
							//Add(tblStudentList);

							tblStudentList.ReloadData();

						}
						else
						{

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
			private void PrepareData()
		{
			try
			{
				 Login.dictionary = NSUserDefaults.StandardUserDefaults.DictionaryForKey(Login.key);
				sms_evening_before = Login.dictionary["sms_evening_before"].ToString().Replace("\"", string.Empty).Trim();
				max_speed = Login.dictionary["max_speed"].ToString().Replace("\"", string.Empty).Trim();
				evening_before = Login.dictionary["evening_before"].ToString().Replace("\"", string.Empty).Trim();
				lang = Login.dictionary["lang"].ToString().Replace("\"", string.Empty).Trim();
				noti_on = Login.dictionary["noti_on"].ToString().Replace("\"", string.Empty).Trim();
				checked_out_on = Login.dictionary["checked_out_on"].ToString().Replace("\"", string.Empty).Trim();
				speed_on = Login.dictionary["speed_on"].ToString().Replace("\"", string.Empty).Trim();
				morning_before = Login.dictionary["morning_before"].ToString().Replace("\"", string.Empty).Trim();
				role = Login.dictionary["role"].ToString().Replace("\"", string.Empty).Trim();
				instant_message = Login.dictionary["instant_message"].ToString().Replace("\"", string.Empty).Trim();
				school_admin_number = Login.dictionary["school_admin_number"].ToString().Replace("\"", string.Empty).Trim();
				sms_checked_out_on = Login.dictionary["sms_checked_out_on"].ToString().Replace("\"", string.Empty).Trim();
				school_admin = Login.dictionary["school_admin"].ToString().Replace("\"", string.Empty).Trim();
				sms_speed_on = Login.dictionary["sms_speed_on"].ToString().Replace("\"", string.Empty).Trim();
				sms_morning_before = Login.dictionary["sms_morning_before"].ToString().Replace("\"", string.Empty).Trim();
				chat_on = Login.dictionary["chat_on"].ToString().Replace("\"", string.Empty).Trim();
				wrong_route_on = Login.dictionary["wrong_route_on"].ToString().Replace("\"", string.Empty).Trim();
				sms_wrong_route_on = Login.dictionary["sms_wrong_route_on"].ToString().Replace("\"", string.Empty).Trim();
				sms_instant_message = Login.dictionary["sms_instant_message"].ToString().Replace("\"", string.Empty).Trim();
				sms_max_speed = Login.dictionary["sms_max_speed"].ToString().Replace("\"", string.Empty).Trim();
				sms_checked_in_on = Login.dictionary["sms_checked_in_on"].ToString().Replace("\"", string.Empty).Trim();
				checked_in_on = Login.dictionary["checked_in_on"].ToString().Replace("\"", string.Empty).Trim();


				SettingsModel	model = new SettingsModel();
				model.speed = max_speed;
				model.speed_sms = sms_max_speed;
				model.evening_notisms = sms_evening_before;
				model.speed = max_speed;
				model.evening_noti = evening_before;

				if (StaticDataModel.CurrentLanguage == "en")
				{
					lang = "0";

				}
				else
				{ 
				lang = "1";
				}
				model.language = lang;
				model.sound_noti = noti_on;
				model.checkedout_noti = checked_out_on;
				model.speed_noti = speed_on;
				model.morning_noti = morning_before;
				model.driver_noti = instant_message;
				model.checkedout_notisms = sms_checked_out_on;
				model.speed_notisms = sms_speed_on;
				model.morning_notisms = sms_morning_before;
				model.sound_chat = chat_on;
				model.wrongroute_noti = wrong_route_on;
				model.wrongroute_notisms = sms_wrong_route_on;
				model.driver_notisms = sms_instant_message;
				model.speed_sms = sms_max_speed;
				model.checkedin_notisms = sms_checked_in_on;
				model.checkedin_noti = checked_in_on;
				Process(model);
			}
			catch (Exception ex)
			{

			}
		}
		private void Process(SettingsModel sm)
		{
			int ResponseCode = 0;
			try
			{
				BTProgressHUD.Show();
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wo
					() =>
					{
						ResponseCode = WebService.SaveSettings(sm);


					}///
		 ).ContinueWith(
					t =>
					{
						if (ResponseCode == 200)
						{
							BTProgressHUD.ShowToast("Settings Saved successfully.", false, 1000);
							SaveUserPrefrences();
							this.DismissModalViewController(false);
						}
						else
						{
							BTProgressHUD.ShowToast("Failed to save settings.", false, 1000);
						}
						BTProgressHUD.Dismiss();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
			}
			catch (Exception ex)
			{

			}

		}
		private void SaveUserPrefrences()
		{
			try
			{
				Login.dictionary = NSUserDefaults.StandardUserDefaults.DictionaryForKey(Login.key);
				string IsRememberedUsernamePwd = Login.dictionary["IsRememberedUsernamePwd"].ToString().Replace("\"", string.Empty).Trim();
				string user_email = Login.dictionary["user_email"].ToString().Replace("\"", string.Empty).Trim();
				string first_name = Login.dictionary["first_name"].ToString().Replace("\"", string.Empty).Trim();
				string middle_name = Login.dictionary["middle_name"].ToString().Replace("\"", string.Empty).Trim();
				string family_name = Login.dictionary["family_name"].ToString().Replace("\"", string.Empty).Trim();
				string contact_number = Login.dictionary["contact_number"].ToString().Replace("\"", string.Empty).Trim();
				string mobile_number = Login.dictionary["mobile_number"].ToString().Replace("\"", string.Empty).Trim();


				//To save the AutoLogin Details
				Login.dictionary = new NSDictionary(Login.UserName, StaticDataModel.UserName,
													Login.UserId, StaticDataModel.UserId,
													Login.ProfilePic, StaticDataModel.ProfilePic,
											  Login.Password, StaticDataModel.UserName,
											 Login.IsRememberedUsernamePwd, IsRememberedUsernamePwd,
											  Login.SchoolName, StaticDataModel.SchoolName,
											  Login.user_email, user_email,
											 Login.first_name, first_name,
											  Login.middle_name, middle_name,
											  Login.family_name, family_name,
											  Login.contact_number, contact_number,
											 Login.mobile_number, mobile_number,
											 Login.sms_evening_before, sms_evening_before,
											Login.max_speed, max_speed,
											 Login.evening_before, evening_before,
											 Login.lang, lang,
											Login.noti_on, noti_on,
											Login.checked_out_on, checked_out_on,
											 Login.speed_on, speed_on,
											Login.morning_before, morning_before,
											 Login.role, role,
											 Login.instant_message, instant_message,
											Login.school_admin_number, school_admin_number,
											 Login.sms_checked_out_on, sms_checked_out_on,
											 Login.school_admin, school_admin,
											 Login.sms_speed_on, sms_speed_on,
											 Login.sms_morning_before, sms_morning_before,
											 Login.chat_on, chat_on,
											Login.wrong_route_on, wrong_route_on,
											 Login.sms_wrong_route_on, sms_wrong_route_on,
											 Login.sms_instant_message, sms_instant_message,
											 Login.sms_max_speed, sms_max_speed,
											 Login.sms_checked_in_on, sms_checked_in_on,
											Login.checked_in_on, checked_in_on);


				NSUserDefaults.StandardUserDefaults.SetValueForKey(Login.dictionary, Login.key);

			}
			catch (Exception ex)
			{

			}
		}
    }
}