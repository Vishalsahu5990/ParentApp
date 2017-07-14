using Foundation;
using System;
using UIKit;
using BigTed;
using System.Threading.Tasks;

namespace ParentApp
{
    public partial class NavigationMenu : BaseController
    {
        public NavigationMenu (IntPtr handle) : base (handle)
        {
        }
		public NavigationMenu(): base(null, null)
		{
		}
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			try
			{Login.dictionary = NSUserDefaults.StandardUserDefaults.DictionaryForKey(Login.key);

			string schoolAdminName = Login.dictionary["school_admin_name"].ToString();
			if (!string.IsNullOrEmpty(schoolAdminName))
			{
				var str = schoolAdminName.Replace("\"", string.Empty).Trim();
StaticDataModel.SchoolAdminName = str.Replace("null", string.Empty).Trim();
			}

			}
			catch (Exception ex)
			{

			}
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			int[] tableItems = new int[] { 1, 2, 3, 4, 5, 6,7,8,9 };
			if (StaticDataModel.CurrentLanguage == "en")
			{
				tblMenuList.RegisterNibForCellReuse(MenuList_tableviewcell.Nib, "Cell");
			}
			else
			{ 
			    tblMenuList.RegisterNibForCellReuse(ar_MenuList_tableviewcell.Nib1, "Cell");
			}


			tblMenuList.Source = new MenuList_tableviewsource(tableItems);
			Add(tblMenuList);

			MenuList_tableviewsource.CellClicked += (object sender, EventArgs e) =>
			 {
				 Menuitemselector(StaticDataModel.CurrentMenuItemPosition);
					
			 };
			AppDelegate.NotificatonCountChanged += (sender, e) =>
			 { 
			tblMenuList.Source = new MenuList_tableviewsource(tableItems);
				 tblMenuList.ReloadData();
			};
			NotificationsController.NotificatonCountChanged += (sender, e) =>
			 { 
			tblMenuList.Source = new MenuList_tableviewsource(tableItems);
tblMenuList.ReloadData();
			};
			ChatWithSchool.NotificatonCountChanged_Chat += (sender, e) =>
			 { 
			tblMenuList.Source = new MenuList_tableviewsource(tableItems);
tblMenuList.ReloadData();
			};
		}
		private void Menuitemselector(int position)
		{
			switch (position)
			{
				case 1:
					SidebarController.CloseMenu();
					break;

				case 2:
					SidebarController.CloseMenu();
					Profile profile = this.Storyboard.InstantiateViewController("Profile") as Profile;
					if (profile != null)
					{
						this.PresentModalViewController(profile, true);

					}
					break;

				case 3:
					SidebarController.CloseMenu();
					StaticDataModel.NotificationBadgeCount_Chat = 0;
					ChatWithSchool chat = this.Storyboard.InstantiateViewController("ChatWithSchool") as ChatWithSchool;
					if (chat != null)
					{
						this.PresentModalViewController(chat, true);

					}

					break;

				case 4:
					SidebarController.CloseMenu();
					StaticDataModel.NotificationBadgeCount = 0;
					NotificationsController noti = this.Storyboard.InstantiateViewController("NotificationsController") as NotificationsController;
					if (noti != null)
					{
						this.PresentModalViewController(noti, true);

					}
					break;

				case 5:
					SidebarController.CloseMenu();
					ReportAbsent ra = this.Storyboard.InstantiateViewController("ReportAbsent") as ReportAbsent;
					if (ra != null)
					{
						this.PresentModalViewController(ra, true);

					}
					break;

				case 6:
					SidebarController.CloseMenu();
					ChangePassword change = this.Storyboard.InstantiateViewController("ChangePassword") as ChangePassword;
					if (change != null)
					{
						this.PresentModalViewController(change, true);

					}
					break;
				case 7:
					
					SidebarController.CloseMenu();
					StaticDataModel.IsFromLanguageMenu = true;
					AppDelegate ap = new AppDelegate();
					if (StaticDataModel.CurrentLanguage == "en")
					{
						
						Home home = ap.MainStoryboard.InstantiateViewController("Home") as Home;
						UIApplication.SharedApplication.KeyWindow.RootViewController = home;
					}
					else
					{ 
						Home home = ap.Main_ArabicStoryboard.InstantiateViewController("Home") as Home;
						UIApplication.SharedApplication.KeyWindow.RootViewController = home;
					}
					break;

				case 8:
					SidebarController.CloseMenu();
					Settings settings = this.Storyboard.InstantiateViewController("Settings") as Settings;
					if (settings != null)
					{
						this.PresentModalViewController(settings, true);

					}
					break;

				case 9:
					SidebarController.CloseMenu();

					if (StaticDataModel.CurrentLanguage == "en")
					{
						 //Create Alert
                    var okCancelAlertController = UIAlertController.Create("Please Confirm", "Do you want to logout?", UIAlertControllerStyle.Alert);

						//Add Actions
						okCancelAlertController.AddAction(UIAlertAction.Create("YES", UIAlertActionStyle.Default, alert =>
						{
							LogoutProcess();
						}));
						okCancelAlertController.AddAction(UIAlertAction.Create("CANCEL", UIAlertActionStyle.Cancel, alert =>
						{

						}));

						//Present Alert
						PresentViewController(okCancelAlertController, true, null);
					}
					else
					{ 
					 //Create Alert
                    var okCancelAlertController = UIAlertController.Create("برجاء التأكيد", "هل تريد الخروج ؟", UIAlertControllerStyle.Alert);

						//Add Actions
						okCancelAlertController.AddAction(UIAlertAction.Create("إلغاء", UIAlertActionStyle.Default, alert =>
						{
							LogoutProcess();
						}));
						okCancelAlertController.AddAction(UIAlertAction.Create("نعم", UIAlertActionStyle.Cancel, alert =>
						{

						}));

						//Present Alert
						PresentViewController(okCancelAlertController, true, null);
					}


					break;
			}

		}
		public async void LogoutProcess()
		{
			string message = string.Empty;

			try
			{
				BTProgressHUD.Show();
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wo
					() =>
					{
					    NSDictionary dictionary = new NSDictionary();
						NSString key1 = new NSString("dict");
						NSUserDefaults.StandardUserDefaults.SetValueForKey(dictionary, key1);
						message = WebService.ParentLogout();
					}).ContinueWith(
					t =>
					{
						if (message == "success")
						{
							Login login = this.Storyboard.InstantiateViewController("Login") as Login;
							if (login != null)
							{
								this.PresentViewController(login, true, null);

							}
						}
						else
						{
						//BTProgressHUD.ShowToast("Something went wrong.", false, 1000);
AppDelegate ap = new AppDelegate();
					if (StaticDataModel.CurrentLanguage == "en")
					{
						
						Login home = ap.MainStoryboard.InstantiateViewController("Login") as Login;
UIApplication.SharedApplication.KeyWindow.RootViewController = home;
					}
					else
					{ 
						Login home = ap.Main_ArabicStoryboard.InstantiateViewController("Login") as Login;
UIApplication.SharedApplication.KeyWindow.RootViewController = home;
					}
						//Login login = this.Storyboard.InstantiateViewController("Login") as Login;
						//	if (login != null)
						//	{
						//		this.PresentViewController(login, true, null);

						//	}
						}


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
    }
}