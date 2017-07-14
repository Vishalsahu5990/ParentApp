using System;
using Foundation;
using SidebarNavigation;
using UIKit;

namespace ParentApp
{
	public partial class BaseController : UIViewController
	{
		// provide access to the sidebar controller to all inheriting controllers
		protected SidebarController SidebarController
		{
			get
			{
				return (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.SidebarController;
			}
		}

		// provide access to the sidebar controller to all inheriting controllers
		protected NavController NavController
		{
			get
			{
				return (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController;
			}
		}

		public BaseController(string nibName, NSBundle bundle) : base(nibName, bundle)
		{
		}
		public BaseController(IntPtr bundle) : base(bundle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(UIImage.FromBundle("menu.png")
					, UIBarButtonItemStyle.Plain
					, (sender, args) =>
					{
						SidebarController.ToggleMenu();
					}), true);
		}
	}
}
