using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using BigTed;
using System.Threading.Tasks;

namespace ParentApp
{
    public partial class NotificationsController : UIViewController
    {
		CustomPopUpView cpuv;
		public List<NotificationModel> model = null;
public static event EventHandler NotificatonCountChanged = delegate { };
		AppDelegate ap = null;
        public NotificationsController (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ap = new AppDelegate();

			Notification_tableviewsource.ViewDetailsClicked += (object sender, DropdownModel e) => 
		 {
			    lblTitle.Text = e.Code;
			    lblDescription.Text = e.Name;
				cpuv = new CustomPopUpView(new CoreGraphics.CGSize(300, 200),uiviewPopup,btnOk);

		 cpuv.PopUp(true, delegate
			{
				Console.WriteLine("cpuv will close");
			});

			 };
			imgBack.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
 		   {
//                if (!StaticDataModel.isFromNotification)
//			   {
//                   this.DismissModalViewController(false);
//}
//			   else
//			   { 
				if (StaticDataModel.CurrentLanguage == "en")
			{
Home home = ap.MainStoryboard.InstantiateViewController("Home") as Home;
UIApplication.SharedApplication.KeyWindow.RootViewController = home;
				//StaticMethods.ChangeLocalization("en")
			}
			else
			{
				Home home = ap.Main_ArabicStoryboard.InstantiateViewController("Home") as Home;
UIApplication.SharedApplication.KeyWindow.RootViewController = home;
				//StaticMethods.ChangeLocalization("ar");
			}
				//}


		   }));

		}
		public override void ViewDidDisappear(bool animated)
		{
             NotificatonCountChanged(this, null);
			base.ViewDidDisappear(animated);
			StaticDataModel.isFromNotification = false;
		}
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			StaticDataModel.NotificationBadgeCount = 0;
			GetAllNotificationsList();
		}
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			StaticDataModel.NotificationBadgeCount = 0;
		}
		private void GetAllNotificationsList()
		{
			model = new List<NotificationModel>();
			try
			{
				BTProgressHUD.Show("Fetching List...");
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

					model = WebService.GetAllNotification();
					}
				///
				).ContinueWith(
					t =>
					{

						if (model != null)
						{
						    tblNotification.Source = new Notification_tableviewsource(model);
							Add(tblNotification);
							tblNotification.ReloadData();

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
    }
}