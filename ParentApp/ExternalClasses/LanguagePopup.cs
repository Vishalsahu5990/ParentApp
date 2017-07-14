using System;
using CoreGraphics;
using UIKit;
namespace ParentApp
{
	public class LanguagePopup: UIView
	{
		public delegate void PopWillCloseHandler();
		public event PopWillCloseHandler PopWillClose;
		UIView customview;
		private UIVisualEffectView effectView = new UIVisualEffectView(UIBlurEffect.FromStyle(UIBlurEffectStyle.Dark));
		private UIButton btnClose = new UIButton(UIButtonType.System);
		public static string Language = string.Empty;
		public LanguagePopup(CGSize size, UIView _customview,UIView uiviewLanguageBorder,
		                     UIButton btnArabic,UIButton btnEnglish,UIButton btnSave,UIButton btnCancel)
		{
			customview = _customview;
			nfloat lx = (UIScreen.MainScreen.Bounds.Width - size.Width) / 2;
			nfloat ly = (UIScreen.MainScreen.Bounds.Height - size.Height) / 2;
			customview.Frame = new CGRect(new CGPoint(lx, ly), size);
			uiviewLanguageBorder.Layer.BorderWidth = 1;
			uiviewLanguageBorder.Layer.BorderColor = UIColor.FromRGB(0.067f, 0.515f, 0.559f).CGColor;
			customview.Layer.BorderColor = UIColor.LightGray.CGColor;
			customview.Layer.BorderWidth = 1;

			if (StaticDataModel.CurrentLanguage == "en")
			{
				btnEnglish.BackgroundColor = UIColor.FromRGB(0.067f, 0.515f, 0.559f);
				btnArabic.BackgroundColor = UIColor.White;

				btnEnglish.SetTitleColor(UIColor.White,UIControlState.Normal);
				btnArabic.SetTitleColor(UIColor.Black, UIControlState.Normal);

				Language = "en";
			}
			else
			{ 
			    btnArabic.BackgroundColor = UIColor.FromRGB(0.067f, 0.515f, 0.559f);
				btnEnglish.BackgroundColor = UIColor.White;

				btnEnglish.SetTitleColor(UIColor.Black, UIControlState.Normal);
				btnArabic.SetTitleColor(UIColor.White, UIControlState.Normal);

				Language = "ar";
			}

			btnEnglish.TouchUpInside += delegate
			{
				btnEnglish.BackgroundColor = UIColor.FromRGB(0.067f, 0.515f, 0.559f);
				btnArabic.BackgroundColor = UIColor.White;

				btnEnglish.SetTitleColor(UIColor.White, UIControlState.Normal);
				btnArabic.SetTitleColor(UIColor.Black, UIControlState.Normal);

				Language = "en";
				
			};
			btnArabic.TouchUpInside += delegate
			{
				btnArabic.BackgroundColor = UIColor.FromRGB(0.067f, 0.515f, 0.559f);
				btnEnglish.BackgroundColor = UIColor.White;

				btnEnglish.SetTitleColor(UIColor.Black, UIControlState.Normal);
				btnArabic.SetTitleColor(UIColor.White, UIControlState.Normal); 

				Language = "ar";
			};

			btnCancel.TouchUpInside += (sender, e) =>
			 { 
			customview.Hidden = true;
                Close();
			};
			btnSave.TouchUpInside += delegate
			{
				customview.Hidden = true;
				Close();
				if (Language == "en")
				{
					StaticDataModel.CurrentLanguage = "en";
					//StaticMethods.ChangeLocalization("en");
					AppDelegate ap = new AppDelegate();
					//Home home = ap.MainStoryboard.InstantiateViewController("Home") as Home;
					//UIApplication.SharedApplication.KeyWindow.RootViewController = home;
				}
				else
				{ 
					StaticDataModel.CurrentLanguage = "ar";
				    //StaticMethods.ChangeLocalization("ar");
					AppDelegate ap = new AppDelegate();
					//Home home = ap.Main_ArabicStoryboard.InstantiateViewController("Home") as Home;
					//UIApplication.SharedApplication.KeyWindow.RootViewController = home;
				}


			};


		}

		public void PopUp(bool animated = true, Action popAnimationFinish = null)
		{
			UIWindow window = UIApplication.SharedApplication.KeyWindow;
			effectView.Frame = window.Bounds;
			window.EndEditing(true);
			window.AddSubview(effectView);
			customview.Hidden = false;
			window.AddSubview(customview);
			effectView.Alpha = 0.4f;

		}

		public void Close(bool animated = true)
		{
			if (animated)
			{
				UIView.Animate(0.15, delegate
				{
					Transform = CGAffineTransform.MakeScale(0.1f, 0.1f);
					effectView.Alpha = 0;
				}, delegate
				{
					this.RemoveFromSuperview();
					effectView.RemoveFromSuperview();
					if (null != PopWillClose) PopWillClose();
				});
			}
			else
			{
				if (null != PopWillClose) PopWillClose();
			}
		}
		//To open this popup
		//CustomPopUpView cpuv = new CustomPopUpView(new CoreGraphics.CGSize(300, 200));
		//cpuv.PopUp(true, delegate
		//	{
		//		Console.WriteLine("cpuv will close");
		//	});	
	}
}
