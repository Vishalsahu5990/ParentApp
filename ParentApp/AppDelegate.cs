using System;
using System.Json;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using KeyboardOverlap.Forms.Plugin.iOSUnified;
using Newtonsoft.Json.Linq;
using PushNotification.Plugin;
using UIKit;

namespace ParentApp
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		NSString key = new NSString("dict");
		NSDictionary dictionary = null;

		//Fires on notification recieve
		public static event EventHandler NotificationReceived = delegate { };
		public static event EventHandler NotificatonCountChanged = delegate { };
		public override UIWindow Window
		{
			get;
			set;
		}
		public Home RootViewController { get { return Window.RootViewController as Home; } }
		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			KeyboardOverlapRenderer.Init();
			CrossPushNotification.Initialize<CrossPushNotificationListener>();
			StaticDataModel.isFromNotification = false;
			StaticDataModel.DeviceHeight = (double)UIScreen.MainScreen.Bounds.Height;
			StaticDataModel.DeviceWidth = (double)UIScreen.MainScreen.Bounds.Width;

			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
					UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
					new NSSet());

				UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
				UIApplication.SharedApplication.ApplicationIconBadgeNumber = -1;
				UIApplication.SharedApplication.RegisterForRemoteNotifications();
			}
			else
			{
				UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);

			}
			///Language selector page
			Login.dictionary2 = NSUserDefaults.StandardUserDefaults.DictionaryForKey(Login.key2);
			if (Login.dictionary2 == null)
			{
				var device = StaticMethods.DeviceType();
				if (device == "ipad")
				{
					var Loginviewcontroller = GetViewController(Main_ArabicStoryboard, "LanguageSelectorIpad");
					SetRootViewController(Loginviewcontroller, false);
				}
				else
				{
					var Loginviewcontroller = GetViewController(Main_ArabicStoryboard, "LanguageSelector");
					SetRootViewController(Loginviewcontroller, false);
				}


			}
			else
			{
				AutoLoginProcess(false);
			}

			StaticDataModel.DeviceId = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();
			Console.WriteLine(StaticDataModel.DeviceId);
			//Changing current language
			//StaticMethods.ChangeLocalization("en");
			//StaticDataModel.CurrentLanguage = "en";

			return true;
		}


		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			var taskID = UIApplication.SharedApplication.BeginBackgroundTask(null);
			Task.Factory.StartNew(() => FinishLongRunningTask(taskID));
		}
		void FinishLongRunningTask(nint taskID)
		{
			Thread.Sleep(2500);
			DidEnterBackground(null);

		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}
		public UIStoryboard MainStoryboard
		{

			get { return UIStoryboard.FromName("Main", NSBundle.MainBundle); }
		}
		public UIStoryboard Main_ArabicStoryboard
		{

			get { return UIStoryboard.FromName("Main_Arabic", NSBundle.MainBundle); }
		}
		public void SetRootViewController(UIViewController rootViewController, bool animate)
		{
			if (animate)
			{
				var transitionType = UIViewAnimationOptions.TransitionFlipFromRight;
				Window.RootViewController = rootViewController;
				UIView.Transition(Window, 0.5, transitionType,
					() => Window.RootViewController = rootViewController,
					null);
			}
			else
			{
				Window.RootViewController = rootViewController;
			}
		}
		public UIViewController GetViewController(UIStoryboard storyboard, string viewControllerName)
		{
			return storyboard.InstantiateViewController(viewControllerName);
		}
		private void AutoLoginProcess(bool isFromNotification)
		{
			try
			{
				if (!isFromNotification)
				{
					//To handle auto login
					dictionary = NSUserDefaults.StandardUserDefaults.DictionaryForKey(key);
					if (dictionary.Count > 0)
					{
						string user_name = dictionary["UserName"].ToString();
						var user_id = dictionary["UserId"].ToString();
						var lang = dictionary["lang"].ToString();
						string profile_pic = dictionary["ProfilePic"].ToString();
						string school_name = dictionary["SchoolName"].ToString();

						if (lang == "0")
						{
							StaticDataModel.CurrentLanguage = "en";
							//StaticMethods.ChangeLocalization("en");
						}
						else
						{
							StaticDataModel.CurrentLanguage = "ar";
							//StaticMethods.ChangeLocalization("ar");
						}

						if (user_name != null)
						{
							StaticDataModel.UserId = Convert.ToInt32(user_id);
							StaticDataModel.UserName = user_name;
							StaticDataModel.ProfilePic = profile_pic;
							StaticDataModel.SchoolName = school_name;
							if (StaticDataModel.CurrentLanguage == "ar")
							{
								var homecontroller = GetViewController(Main_ArabicStoryboard, "Home");
								SetRootViewController(homecontroller, false);
							}
							else
							{
								var homecontroller = GetViewController(MainStoryboard, "Home");
								SetRootViewController(homecontroller, false);

							}

						}
					}
					else
					{
						if (StaticDataModel.CurrentLanguage == "ar")
						{
							var Loginviewcontroller = GetViewController(Main_ArabicStoryboard, "Login");
							SetRootViewController(Loginviewcontroller, false);
						}
						else
						{
							var Loginviewcontroller = GetViewController(MainStoryboard, "Login");
							SetRootViewController(Loginviewcontroller, false);

						}

					}
				}
				else
				{
					// notification tapp


					//To handle auto login
					dictionary = NSUserDefaults.StandardUserDefaults.DictionaryForKey(key);
					if (dictionary.Count > 0)
					{
						string user_name = dictionary["UserName"].ToString();
						var user_id = dictionary["UserId"].ToString();
						var lang = dictionary["lang"].ToString();
						string profile_pic = dictionary["ProfilePic"].ToString();
						string school_name = dictionary["SchoolName"].ToString();

						if (lang == "0")
						{
							StaticDataModel.CurrentLanguage = "en";
							//StaticMethods.ChangeLocalization("en");
						}
						else
						{
							StaticDataModel.CurrentLanguage = "ar";
							//StaticMethods.ChangeLocalization("ar");
						}

						if (user_name != null)
						{
							string _crontroller = "NotificationsController";
							if (StaticDataModel.model.noti_type == "chat")
							{
								_crontroller = "ChatWithSchool";
							}
							StaticDataModel.UserId = Convert.ToInt32(user_id);
							StaticDataModel.UserName = user_name;
							StaticDataModel.ProfilePic = profile_pic;
							StaticDataModel.SchoolName = school_name;
							if (StaticDataModel.CurrentLanguage == "ar")
							{
								var homecontroller = GetViewController(Main_ArabicStoryboard, _crontroller);
								SetRootViewController(homecontroller, false);
							}
							else
							{
								var homecontroller = GetViewController(MainStoryboard, _crontroller);
								SetRootViewController(homecontroller, false);

							}

						}
					}
					else
					{
						if (StaticDataModel.CurrentLanguage == "ar")
						{
							var Loginviewcontroller = GetViewController(Main_ArabicStoryboard, "Login");
							SetRootViewController(Loginviewcontroller, false);
						}
						else
						{
							var Loginviewcontroller = GetViewController(MainStoryboard, "Login");
							SetRootViewController(Loginviewcontroller, false);

						}

					}
				}
			}
			catch (Exception ex)
			{
				if (StaticDataModel.CurrentLanguage == "ar")
				{
					var Loginviewcontroller = GetViewController(Main_ArabicStoryboard, "Login");
					SetRootViewController(Loginviewcontroller, false);
				}
				else
				{
					var Loginviewcontroller = GetViewController(MainStoryboard, "Login");
					SetRootViewController(Loginviewcontroller, false);

				}
			}
		}
		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			if (CrossPushNotification.Current is IPushNotificationHandler)
			{
				((IPushNotificationHandler)CrossPushNotification.Current).OnRegisteredSuccess(deviceToken);
			}
			// Get current device token
			var DeviceToken = deviceToken.Description;
			if (!string.IsNullOrWhiteSpace(DeviceToken))
			{
				DeviceToken = DeviceToken.Trim('<').Trim('>');
				DeviceToken = DeviceToken.Replace(" ", "");
				StaticDataModel.DeviceToken = DeviceToken.ToString();
				Console.WriteLine(DeviceToken);
			}

			// Get previous device token
			var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");

			// Has the token changed?
			if (string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(DeviceToken))
			{
				//TODO: Put your own logic here to notify your server that the device token has changed/been created!
			}

			// Save new device token 
			NSUserDefaults.StandardUserDefaults.SetString(DeviceToken, "PushDeviceToken");
		}

		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
			if (CrossPushNotification.Current is IPushNotificationHandler)
			{
				((IPushNotificationHandler)CrossPushNotification.Current).OnErrorReceived(error);
			}
		}

		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary options, Action<UIBackgroundFetchResult> completionHandler)
		{
			try
			{




				if (null != options && options.ContainsKey(new NSString("msg")))
				{
					var data = options["msg"];
					var json = JObject.Parse(data.ToString());

					NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

					string alert = string.Empty;
					string sound = string.Empty;
					int badge = -1;

					if (json != null)
					{
						StaticDataModel.model = new NotificationModel();

						StaticDataModel.model.noti_type = json["noti_type"].ToString();
						if (StaticDataModel.model.noti_type != "stop_service")
						{
							StaticDataModel.model.noti_id = json["noti_id"].ToString();
							StaticDataModel.model.noti_type = json["noti_type"].ToString();
							StaticDataModel.model.date = json["date"].ToString();
							StaticDataModel.model.msg = json["msg"].ToString();

							if (StaticDataModel.model.noti_type == "checked_out" || StaticDataModel.model.noti_type == "checked_in")
							{
								NotificationReceived(this, null);
							}
							if (StaticDataModel.model.noti_type != "chat")
							{
								StaticDataModel.NotificationBadgeCount = StaticDataModel.NotificationBadgeCount + 1;
							}
							else
							{
								StaticDataModel.NotificationBadgeCount_Chat = StaticDataModel.NotificationBadgeCount_Chat + 1;
							}
						}
					}

				}


				//Fires event on ipushnotification service class
				if (CrossPushNotification.Current is IPushNotificationHandler)
				{
					((IPushNotificationHandler)CrossPushNotification.Current).OnMessageReceived(options);
				}
				if (application.ApplicationState == UIApplicationState.Inactive || application.ApplicationState == UIApplicationState.Background)
				{
					StaticDataModel.isFromNotification = true;
					//opened from a push notification when the app was on background
					if (StaticDataModel.model != null)
					{
						if (StaticDataModel.model.noti_type != "stop_service")
							OnNotificationTap();
					}

				}
				NotificatonCountChanged(this, null);
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
		private void OnNotificationTap()
		{
			try
			{
				///Language selector page
				Login.dictionary2 = NSUserDefaults.StandardUserDefaults.DictionaryForKey(Login.key2);
				if (Login.dictionary2 == null)
				{
					var device = StaticMethods.DeviceType();
					if (device == "ipad")
					{
						var Loginviewcontroller = GetViewController(Main_ArabicStoryboard, "LanguageSelectorIpad");
						SetRootViewController(Loginviewcontroller, false);
					}
					else
					{
						var Loginviewcontroller = GetViewController(Main_ArabicStoryboard, "LanguageSelector");
						SetRootViewController(Loginviewcontroller, false);
					}


				}
				else
				{
					AutoLoginProcess(true);
				}
			}
			catch (Exception ex)
			{

			}
		}
		public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
		{
			application.RegisterForRemoteNotifications();
		}



		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
			if (CrossPushNotification.Current is IPushNotificationHandler)
			{
				((IPushNotificationHandler)CrossPushNotification.Current).OnMessageReceived(userInfo);
			}
		}


	}
}

