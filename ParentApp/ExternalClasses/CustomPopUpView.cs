using System;
using CoreGraphics;
using UIKit;
namespace ParentApp
{
	public class CustomPopUpView : UIView
	{
		public delegate void PopWillCloseHandler();
		public event PopWillCloseHandler PopWillClose;
		UIView customview;
		private UIVisualEffectView effectView = new UIVisualEffectView(UIBlurEffect.FromStyle(UIBlurEffectStyle.Dark));
		private UIButton btnClose = new UIButton(UIButtonType.System);

		public CustomPopUpView(CGSize size,UIView _customview,UIButton button)
		{
			customview = _customview;
			nfloat lx = (UIScreen.MainScreen.Bounds.Width - size.Width) / 2;
			nfloat ly = (UIScreen.MainScreen.Bounds.Height - size.Height) / 2;
			customview.Frame = new CGRect(new CGPoint(lx, ly), size);
			button.TouchUpInside += delegate
			{
				customview.Hidden = true;
				Close();
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
			else {
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
