using Foundation;
using System;
using UIKit;
using BigTed;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParentApp
{
	public partial class ForgotPassword : UIViewController
	{
		private UIView activeview;             // Controller that activated the keyboard
		private float scroll_amount = 0.0f;    // amount to scroll 
		private float bottom = 0.0f;           // bottom point
		private float offset = 10.0f;          // extra offset
		private bool moveViewUp = false;           // which direction are we moving
		public ForgotPassword(IntPtr handle) : base(handle)
		{
		}
		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Keyboard popup
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

			// Keyboard Down
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.WillHideNotification, KeyBoardDownNotification);

			PrepareUI();
			imgBack.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
		   {
			   this.DismissModalViewController(false);

		   }));
		}
		private void KeyBoardUpNotification(NSNotification notification)
		{
			try
			{

				// get the keyboard size
				var r = UIKeyboard.BoundsFromNotification(notification);

				// Find what opened the keyboard
				foreach (UIView view in this.View.Subviews)
				{
					//if (view.IsFirstResponder)
					activeview = view;
				}
				// Bottom of the controller = initial position + height + offset      
				bottom = (float)(activeview.Frame.Y + activeview.Frame.Height + offset);

				// Calculate how far we need to scroll
				scroll_amount = (float)(r.Height - (View.Frame.Size.Height - bottom));

				// Perform the scrolling
				if (scroll_amount > 0)
				{
					moveViewUp = true;
					ScrollTheView(moveViewUp);
				}
				else
				{
					moveViewUp = false;
				}
			}
			catch (Exception ex)
			{

			}

		}
		private void KeyBoardDownNotification(NSNotification notification)
		{
			if (moveViewUp) { ScrollTheView(false); }
		}
		private void ScrollTheView(bool move)
		{

			// scroll the view up or down
			UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
			UIView.SetAnimationDuration(0.3);

			var frame = View.Frame;

			if (move)
			{
				frame.Y -= scroll_amount;
			}
			else
			{
				frame.Y += scroll_amount;
				scroll_amount = 0;
			}

			View.Frame = frame;
			UIView.CommitAnimations();
		}
		partial void BtnSubmit_TouchUpInside(UIButton sender)
		{
			if (!StaticMethods.IsConnectedToInternet())
			{
				BTProgressHUD.ShowToast("You are not connected to internet.", false, 1000);

			}
			else
			{

				if (IsValidate())
				{
					Process()
;
				}

			}
		}

		private void PrepareUI()
		{
			try
			{

				StaticMethods.SetPadding(txtEmail, 20);
				StaticMethods.SetTextFieldLeftIcon(txtEmail, "email.png");
				txtEmail.Layer.BorderColor = UIColor.LightGray.CGColor;
				txtEmail.Layer.BorderWidth = 1;

			}
			catch (Exception ex)
			{

			}
		}
		private bool IsValidate()
		{
			if (!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
			{
				BTProgressHUD.ShowToast("Email is not in valid format.", false, 1000);
				return false;
			}
			else
			{
				return true;
			}
		}

		private void Process()
		{
			int ResponseCode = 0;
			try
			{
				BTProgressHUD.Show();
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wo
					() =>
					{
						ResponseCode = WebService.ForgotPassword(txtEmail.Text);

					}///
		  ).ContinueWith(
					t =>
					{
						if (ResponseCode == 200)
						{
							BTProgressHUD.ShowToast("A link was sent on your email address please check your inbox to change  your password.", false, 1000);
						}
						else
						{
							BTProgressHUD.ShowToast("Please enter a valid email.", false, 1000);
						}
						BTProgressHUD.Dismiss();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
			}
			catch (Exception ex)
			{

			}

		}
	}
}