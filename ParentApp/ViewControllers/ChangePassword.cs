using Foundation;
using System;
using UIKit;
using BigTed;
using System.Threading.Tasks;

namespace ParentApp
{
    public partial class ChangePassword : UIViewController
    {
private UIView activeview;             // Controller that activated the keyboard
private float scroll_amount = 0.0f;    // amount to scroll 
private float bottom = 0.0f;           // bottom point
private float offset = 10.0f;          // extra offset
private bool moveViewUp = false;           // which direction are we moving
        public ChangePassword (IntPtr handle) : base (handle)
        {
        }
		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Keyboard popu
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

			// Keyboard Dow
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.WillHideNotification, KeyBoardDownNotification);

			PrepareUI();
			imgBack.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
		   {
			   this.DismissModalViewController(false);

		   }));
			txtNewPassword.ShouldReturn += (textField) => 
			{
				
					textField.ResignFirstResponder();
				return true;

			};
			txtCurrentPassword.ShouldReturn += (textField) => 
			{
				
					textField.ResignFirstResponder();
				return true;

			};
			txtConfirmPassword.ShouldReturn += (textField) => 
			{
				
					textField.ResignFirstResponder();
				return true;

			};
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
			if (view.IsFirstResponder)
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

					Process();

				}

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
					ResponseCode = WebService.ChangePassword(StaticDataModel.UserName,txtCurrentPassword.Text,txtNewPassword.Text);

					}///
		  ).ContinueWith(
					t =>
					{
						if (ResponseCode == 200)
						{
							BTProgressHUD.ShowToast("Password has been changed successfully.", false, 1000);
						StudentList home = this.Storyboard.InstantiateViewController("StudentList") as StudentList;
							if (home != null)
							{
							this.PresentModalViewController(home, false);
							}
						}
						else
						{
							BTProgressHUD.ShowToast("Username or password does not match.", false, 1000);
						}
						BTProgressHUD.Dismiss();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
			}
			catch (Exception ex)
			{

			}

		}
		private void PrepareUI()
		{
			try
			{


				//StaticMethods.SetTextFieldLeftIcon(txtCurrentPassword, "password.png");
				txtCurrentPassword.Layer.BorderColor = UIColor.LightGray.CGColor;
				txtCurrentPassword.Layer.BorderWidth = 1;
				StaticMethods.SetPadding(txtCurrentPassword, 40);

				StaticMethods.SetPadding(txtNewPassword, 40);
				//StaticMethods.SetTextFieldLeftIcon(txtNewPassword, "password.png");
				txtNewPassword.Layer.BorderColor = UIColor.LightGray.CGColor;
				txtNewPassword.Layer.BorderWidth = 1;

				StaticMethods.SetPadding(txtConfirmPassword, 40);
				//StaticMethods.SetTextFieldLeftIcon(txtConfirmPassword, "password.png");
				txtConfirmPassword.Layer.BorderColor = UIColor.LightGray.CGColor;
				txtConfirmPassword.Layer.BorderWidth = 1;

			}
			catch (Exception ex)
			{

			}
		}
		private bool IsValidate()
		{
			if (txtCurrentPassword.Text == string.Empty)
			{
				BTProgressHUD.ShowToast("Please fill required fields.", false, 1000);
				return false;

			}
			else if (txtNewPassword.Text == string.Empty)
			{

				BTProgressHUD.ShowToast("Please fill required fields.", false, 1000);
				return false;

			}
			else if (txtConfirmPassword.Text == string.Empty)
			{

				BTProgressHUD.ShowToast("Please fill required fields.", false, 1000);
				return false;

			}
			else if (txtConfirmPassword.Text != txtNewPassword.Text)
			{

				BTProgressHUD.ShowToast("New password and confirm password did not mached.", false, 1000);
				return false;

			}
			else
			{
				return true;
			}
		}
	}
}