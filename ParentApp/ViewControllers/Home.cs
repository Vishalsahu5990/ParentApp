using Foundation;
using System;
using UIKit;

namespace ParentApp
{
    public partial class Home : UIViewController
    {
		public NavController NavController { get; set; }
		public SidebarNavigation.SidebarController SidebarController { get; set; }
        public Home (IntPtr handle) : base (handle)
        {
        }
		public Home() : base (null,null)
        {
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			PrepareNavigationController();
;
		}
		private void PrepareNavigationController()
		{ 
		try
			{
				var mediatorViewController = (StudentList)Storyboard.InstantiateViewController("StudentList");
				var Navigationmenucontroller = (NavigationMenu)Storyboard.InstantiateViewController("NavigationMenu");
				var home = (Home)Storyboard.InstantiateViewController("Home");
				NavController = new NavController();
				NavController.PushViewController(mediatorViewController, false);
				SidebarController = new SidebarNavigation.SidebarController(this, NavController, Navigationmenucontroller);
				SidebarController.MenuWidth = 250;
				SidebarController.ReopenOnRotate = false;
				if (StaticDataModel.CurrentLanguage == "ar")
				{
					SidebarController.MenuLocation = SidebarNavigation.SidebarController.MenuLocations.Right;
				}
				else
				{
					SidebarController.MenuLocation = SidebarNavigation.SidebarController.MenuLocations.Left;
				}


			}
			catch (Exception ex)
			{

			}
		}

    }
}