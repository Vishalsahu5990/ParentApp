using Foundation;
using System;
using UIKit;

namespace ParentApp
{
    public partial class LanguageSelectorIpad : UIViewController
	{
		LanguagePopup cpuv;
		public LanguageSelectorIpad(IntPtr handle) : base(handle)
		{
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);


			//uiview.Hidden = false;
			cpuv = new LanguagePopup(new CoreGraphics.CGSize(uiview.Frame.Width, uiview.Frame.Height), uiview, uiviewLanguageBorder, btnArabic, btnEnglish, btnSave,btnCancel);
			cpuv.PopWillClose += Cpuv_PopWillClose;
			cpuv.PopUp(true, delegate
			   {
				   Console.WriteLine("cpuv will close");
				   this.DismissViewController(false, null);
			   });


		}
		void Cpuv_PopWillClose()
		{

			Console.WriteLine("cpuv will close");
			AppDelegate ap = new AppDelegate();
			if (StaticDataModel.CurrentLanguage == "en")
			{

				Login home = ap.MainStoryboard.InstantiateViewController("Login") as Login;
				UIApplication.SharedApplication.KeyWindow.RootViewController = home;
				//StaticMethods.ChangeLocalization("en")
			}
			else
			{
				Login home = ap.Main_ArabicStoryboard.InstantiateViewController("Login") as Login;
				UIApplication.SharedApplication.KeyWindow.RootViewController = home;
				//StaticMethods.ChangeLocalization("ar");
			}


		}
		private void ShowLanguagePopup()
		{
			try
			{
				Login.dictionary2 = NSUserDefaults.StandardUserDefaults.DictionaryForKey(Login.key2);
				var IsFirstTime = Login.dictionary2["IsFirstTime"].ToString().Replace("\"", string.Empty).Trim();

			}
			catch (Exception ex)
			{

			}
		}
	}
}