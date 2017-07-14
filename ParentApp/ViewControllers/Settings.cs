using Foundation;
using System;
using UIKit;
using BigTed;
using System.Threading.Tasks;

namespace ParentApp
{
    public partial class Settings : UIViewController
    {
public static event EventHandler SaveSettings = delegate { };
		string sms_evening_before = string.Empty;
		string max_speed= string.Empty;
		string evening_before= string.Empty;
		string lang= string.Empty;
	    string noti_on= string.Empty;
	    string checked_out_on= string.Empty;
		string speed_on= string.Empty;
		string morning_before= string.Empty;
		string role= string.Empty;
		string instant_message= string.Empty;
		string school_admin_number= string.Empty;
		string sms_checked_out_on= string.Empty;
		string school_admin= string.Empty;
		string sms_speed_on= string.Empty;
		string sms_morning_before= string.Empty;
		string chat_on= string.Empty;
		string wrong_route_on= string.Empty;
		string sms_wrong_route_on= string.Empty;
		string sms_instant_message= string.Empty;
		string sms_max_speed= string.Empty;
		string sms_checked_in_on= string.Empty;
        string checked_in_on= string.Empty;
		SettingsModel model = null;
		public Settings (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLayoutSubviews()
		{
			this.scrollview.ContentSize = contentview.Bounds.Size;
		}
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			SetData();
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			PrepareUI();
			ClickEvents();
			imgBack.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
		   {
			   this.DismissModalViewController(false);

		   }));
			sliderSpeed.ValueChanged += (sender, e) =>
			 {
				try
				 {
					 var value = sliderSpeed.Value;
					 var roundoff = Math.Round(value);
					 lblSpeed.Text = roundoff.ToString() + "km/h";
					 max_speed = roundoff.ToString();
				 }
				 catch (Exception ex)
				 {

				 }

			};
			sliderSms.ValueChanged += (sender, e) =>
			 {
				 try
				 {
					 var value = sliderSms.Value;
					 var roundoff = Math.Round(value);
					lblmsg_speed.Text = roundoff.ToString() + "km/h"
						;
					sms_max_speed = roundoff.ToString(); 
				}
				 catch (Exception ex)
				 {

				 }

			 };
		}


        partial void BtnUpdate_TouchUpInside(UIButton sender)
		{
			PrepareData();
			SaveUserPrefrences();
		}
		private void PrepareData()
		{ 
		try
			{
				
				model = new SettingsModel();
				model.speed = lblSpeed.Text;
				model.speed_sms = lblmsg_speed.Text;
				model.evening_notisms = sms_evening_before;
				model.speed = max_speed;
				model.evening_noti = evening_before;
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
				if (lang == "0")
				{
					StaticDataModel.CurrentLanguage = "en";
				}
				else
				{ 
				StaticDataModel.CurrentLanguage = "ar";
				}
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
                        SaveSettings(this, null);
							BTProgressHUD.ShowToast("Settings Saved successfully.", false, 1000);
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
		private void SetData()
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

				lblSpeed.Text = max_speed;
				lblmsg_speed.Text = sms_max_speed;
				sliderSpeed.Value =float.Parse( max_speed);
				sliderSms.Value = float.Parse(sms_max_speed);
				if (sms_evening_before == "0")
				{
					 btnsms_e_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					 btnsms_e_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				else
				{ 
				    btnsms_e_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnsms_e_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}

				if (evening_before == "0")
				{
					 btnEveningon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					BtnEveningoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{
					BtnEveningoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnEveningon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				
				}
				if (lang == "0")
				{
					 btnEnon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnEnoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{
					btnEnoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnEnon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				if (noti_on == "0")
				{
						btnNotificationon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnNotificationoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{
					 btnNotificationoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					 btnNotificationon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				if (checked_out_on == "0")
				{
					btnCheckedOuton.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnCheckedOutoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{
					 btnCheckedOutoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					 btnCheckedOuton.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				if (speed_on == "0")
				{
					 sliderSpeed.Hidden = false;
					btnBusspeedon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnBusspeedoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{
					sliderSpeed.Hidden = true;
					btnBusspeedoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnBusspeedon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				if (morning_before == "0")
				{
					btnMorningon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnMorningoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{
					 btnMorningoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					 btnMorningon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}

				if (instant_message == "0")
				{
					btnDriveon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnDriveoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{
					 btnDriveoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					 btnDriveon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				if (sms_checked_out_on == "0")
				{
					btnsms_co_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnsms_co_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{
					 btnsms_co_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					 btnsms_co_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				if (sms_speed_on == "0")
				{
					btnsms_bs_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnsms_bs_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{
					 btnsms_bs_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnsms_bs_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				if (sms_morning_before == "0")
				{
					 btnsms_m_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnsms_m_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{
					 btnsms_m_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnsms_m_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				if (chat_on == "0")
				{
					btnChaton.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnChatoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{

				    btnChatoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnChaton.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				if (wrong_route_on == "0")
				{
					btnBusatwrongon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnBusatwrongoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{
					 btnBusatwrongoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnBusatwrongon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				if (sms_wrong_route_on == "0")
				{
					 btnsms_b_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnsms_b_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				else
				{
					 btnsms_b_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnsms_b_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				if (sms_instant_message == "0")
				{
					 btnsms_d_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnsms_d_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				else
				{
					 btnsms_d_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					 btnsms_d_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}

				if (sms_checked_in_on == "0")
				{
					 btnsms_ci_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnsms_ci_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);

				}
				else
				{
					 btnsms_ci_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnsms_ci_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				if (checked_in_on == "0")
				{
					btnCheckedInon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnCheckedInoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				else
				{
					 btnCheckedInoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
					btnCheckedInon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				}
				}
			catch (Exception ex)
			{

			}
		}
		private void ClickEvents()
		{
			
			btnL1.TouchUpInside += (sender, e) =>
			 {
				 lang = "0";
				 btnEnon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
btnEnoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			
				 if (StaticDataModel.CurrentLanguage == "ar")
				 { 
btnEnoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
btnEnon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
					 lang = "1";
				}
 
				  };
			btnL2.TouchUpInside += (sender, e) =>
			 {
				lang = "1";
				btnEnoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				btnEnon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
				 if (StaticDataModel.CurrentLanguage == "ar")
				 { 
 btnEnon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
btnEnoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
					 lang = "0";
				}
			 };

			btnN1.TouchUpInside += (sender, e) =>
			 {
				noti_on = "0";
				btnNotificationon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				btnNotificationoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnN2.TouchUpInside += (sender, e) =>
			 {
				 noti_on= "1";
				 btnNotificationoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnNotificationon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };

			btnCh1.TouchUpInside += (sender, e) =>
			 {
				 chat_on = "0";
				 btnChaton.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				btnChatoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };

			btnCh2.TouchUpInside += (sender, e) =>
			 {
				 chat_on = "1";
				 btnChatoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnChaton.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };

			btnCi1.TouchUpInside += (sender, e) =>
			 {
				checked_in_on = "0";
				btnCheckedInon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				btnCheckedInoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnCi2.TouchUpInside += (sender, e) =>
			 {
				checked_in_on= "1";
				 btnCheckedInoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnCheckedInon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };

			btnCo1.TouchUpInside += (sender, e) =>
			 {
				checked_out_on= "0";
				btnCheckedOuton.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				btnCheckedOutoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnCo2.TouchUpInside += (sender, e) =>
			 {
				 checked_out_on= "1";
				 btnCheckedOutoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnCheckedOuton.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };

			btnS1.TouchUpInside += (sender, e) =>
			 {
				speed_on= "0";
				sliderSpeed.Hidden = false;
				btnBusspeedon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				btnBusspeedoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnS2.TouchUpInside += (sender, e) =>
			 {
				speed_on= "1";
				sliderSpeed.Hidden = true;
				 btnBusspeedoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnBusspeedon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };

			btnB1.TouchUpInside += (sender, e) =>
			 {
				wrong_route_on= "0";
				btnBusatwrongon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				btnBusatwrongoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnB2.TouchUpInside += (sender, e) =>
			 {
				wrong_route_on = "1";
				 btnBusatwrongoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnBusatwrongon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };

			btnD1.TouchUpInside += (sender, e) =>
			 {
				instant_message= "0";
				btnDriveon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				btnDriveoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnD2.TouchUpInside += (sender, e) =>
			 {
				instant_message= "1";
				 btnDriveoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnDriveon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };

			btnM1.TouchUpInside += (sender, e) =>
			 {
				morning_before= "0";
				btnMorningon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				btnMorningoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnM2.TouchUpInside += (sender, e) =>
			 {
				morning_before= "1";
				 btnMorningoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnMorningon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };

			btnE1.TouchUpInside += (sender, e) =>
			 {
				evening_before= "0";
				 btnEveningon.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 BtnEveningoff.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnE2.TouchUpInside += (sender, e) =>
			 {
				evening_before= "1";
				BtnEveningoff.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				btnEveningon.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			//other events
			btnsms_ci1.TouchUpInside += (sender, e) =>
			 {
				 sms_checked_in_on= "0";
				 btnsms_ci_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_ci_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_ci2.TouchUpInside += (sender, e) =>
			 {
				 sms_checked_in_on= "1";
				 btnsms_ci_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_ci_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_co1.TouchUpInside += (sender, e) =>
			 {
				sms_checked_out_on= "0";
				btnsms_co_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				btnsms_co_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_co2.TouchUpInside += (sender, e) =>
			 {
				 sms_checked_out_on= "1";
				 btnsms_co_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_co_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_bs1.TouchUpInside += (sender, e) =>
			 {
				 sms_speed_on= "0";
				 btnsms_bs_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_bs_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_bs2.TouchUpInside += (sender, e) =>
			 {
				 sms_speed_on = "1";
				 btnsms_bs_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_bs_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_b1.TouchUpInside += (sender, e) =>
			 {
				 sms_wrong_route_on = "0";
				 btnsms_b_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_b_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_b2.TouchUpInside += (sender, e) =>
			 {
				sms_wrong_route_on = "1";
				 btnsms_b_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_b_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_d1.TouchUpInside += (sender, e) =>
			 {
				 sms_instant_message = "0";
				 btnsms_d_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_d_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_d2.TouchUpInside += (sender, e) =>
			 {
				 sms_instant_message = "1";
				 btnsms_d_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_d_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_m1.TouchUpInside += (sender, e) =>
			 {
				 sms_morning_before = "0";
				 btnsms_m_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_m_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_m2.TouchUpInside += (sender, e) =>
			 {
				 sms_morning_before = "1";
				 btnsms_m_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_m_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_e1.TouchUpInside += (sender, e) =>
			 {
				 sms_evening_before = "0";
				 btnsms_e_on.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_e_off.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };
			btnsms_e2.TouchUpInside += (sender, e) =>
			 {
				 sms_evening_before = "1";
				 btnsms_e_off.BackgroundColor = new UIColor(red: 0.102f, green: 0.606f, blue: 0.395f, alpha: 1f);
				 btnsms_e_on.BackgroundColor = new UIColor(red: 0.827f, green: 0.817f, blue: 0.817f, alpha: 1f);
			 };

		}
		private void PrepareUI()
		{
			try
			{
				contentview.Layer.BorderColor = UIColor.LightGray.CGColor;
				contentview.Layer.BorderWidth = 1;
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
				                                    Login. ProfilePic, StaticDataModel.ProfilePic,
											  Login.Password, StaticDataModel.UserName,
											 Login. IsRememberedUsernamePwd, IsRememberedUsernamePwd,
											  Login.SchoolName, StaticDataModel.SchoolName,
											  Login.user_email, user_email,
											 Login. first_name,first_name,
											  Login.middle_name, middle_name,
											  Login.family_name, family_name,
											  Login.contact_number, contact_number,
											 Login. mobile_number, mobile_number,
											 Login.sms_evening_before, sms_evening_before,
											Login. max_speed, max_speed,
											 Login.evening_before, evening_before,
											 Login.lang, lang,
											Login. noti_on,noti_on,
											Login. checked_out_on,checked_out_on,
											 Login.speed_on,speed_on,
											Login. morning_before, morning_before,
											 Login.role, role,
											 Login.instant_message, instant_message,
											Login. school_admin_number,school_admin_number,
											 Login.sms_checked_out_on, sms_checked_out_on,
											 Login.school_admin, school_admin,
											 Login.sms_speed_on, sms_speed_on,
											 Login.sms_morning_before,sms_morning_before,
											 Login.chat_on,chat_on,
											Login. wrong_route_on, wrong_route_on,
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