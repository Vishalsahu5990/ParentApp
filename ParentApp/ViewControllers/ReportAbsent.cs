using Foundation;
using System;
using UIKit;
using System.Threading.Tasks;
using BigTed;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Drawing;

namespace ParentApp
{
    public partial class ReportAbsent : UIViewController
    {

private UIView activeview;             // Controller that activated the keyboard
private float scroll_amount = 0.0f;    // amount to scroll 
private float bottom = 0.0f;           // bottom point
private float offset = 10.0f;          // extra offset
private bool moveViewUp = false;           // which direction are we moving

		public List<StudentModel> model = null;
		SlideCalendar calendar;
		public static int SelectedStudentId = 0;
		List<int> SelectedDates;
		DateTime date;
		CoreGraphics.CGRect _size;
        public ReportAbsent (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			GetDropdownList();

			// Keyboard popup
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

			// Keyboard Down
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.WillHideNotification, KeyBoardDownNotification);

			//model = WebService.GetStudentByParentId();
			GetPickerUi.SetupPicker(txtDropdown, model);
			PrepareUI();

			//To add calendar 
			calendar = new SlideCalendar(new CoreGraphics.CGPoint(0, 20), DateTime.Now, new DateTime(1984, 8, 15), new DateTime(2020, 8, 15));
			calendar.OnDaySelected += Calendar_OnDaySelected;

			uiviewCalendar.AddSubview(calendar);

			calendar.NextMonth += Calendar_NextMonth;
			calendar.PreviousMonth += Calendar_PreviousMonth;

			imgBack.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
		   {
			   this.DismissModalViewController(false);

		   }));
			txtReason.ShouldReturn += (textField) => 
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
    foreach (UIView view in this.View.Subviews) {
	//if (view.IsFirstResponder)
	activeview = view;
}
// Bottom of the controller = initial position + height + offset      
bottom = (float)(activeview.Frame.Y + activeview.Frame.Height + offset);

// Calculate how far we need to scroll
scroll_amount = (float)(r.Height - (View.Frame.Size.Height - bottom)) ;
 
			 // Perform the scrolling
    if (scroll_amount > 0) {
	moveViewUp = true;
	ScrollTheView(moveViewUp);
} else {
	moveViewUp = false; }		}
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
public override void ViewDidLayoutSubviews()
{
			
			//this.scrollView.ContentSize = _uiview.Bounds.Size;
	
	}


        void Calendar_OnDaySelected (DateTime selectedDate, List<int> ListofSelectedDates)
		{
			SelectedDates = ListofSelectedDates;date = selectedDate;
		}

		partial void BtnNext_TouchUpInside(UIButton sender)
		{
			calendar.OnArrowTouchUp(sender);
		}

		partial void BtnPrevious_TouchUpInside(UIButton sender)
		{
			calendar.OnArrowTouchUp(sender);
		}
		private bool IsValidate()
		{
			bool flag = true;
		try
			{
				if (SelectedStudentId == 0)
				{
					flag = false;
					BTProgressHUD.ShowToast("Select student.", false, 1000);
					return flag;
				}

				else if (SelectedDates != null)
				{
					if (SelectedDates.Count <= 0)
					{

						flag = false;
						BTProgressHUD.ShowToast("Select dates.", false, 1000);
						return flag;
					}

				}
				else
				{ 
				    flag = false;
					BTProgressHUD.ShowToast("Select dates.", false, 1000);
					return flag;
				}
				 if (string.IsNullOrEmpty(txtReason.Text))
				{
					BTProgressHUD.ShowToast("Enter reason.", false, 1000);
					flag = false;
					return flag;
				}
			}
			catch (Exception ex)
			{
				return flag;
			}
			return flag;
		}



		private void PrepareUI()
		{
			try
			{
				txtDropdown.Layer.BorderColor = UIColor.LightGray.CGColor;
				txtDropdown.Layer.BorderWidth = 1;
				StaticMethods.SetPadding(txtDropdown, 40);

				txtReason.Layer.BorderColor = UIColor.LightGray.CGColor;
				txtReason.Layer.BorderWidth = 1;
				StaticMethods.SetPadding(txtReason, 40);

				lblDescription.TextColor = UIColor.LightGray;
				txtDropdown.UserInteractionEnabled = true;


				lblYearTitle.Text = DateTime.Now.ToString("Y").Trim(',');


			}
			catch (Exception ex)
			{

			}
		}

				partial void BtnSubmit_TouchUpInside(UIButton sender)
		{

			 var okCancelAlertController = UIAlertController.Create("Please Confirm", "* Now you are making that your student will not going to school on selected days.", UIAlertControllerStyle.Alert);

			//Add Actions
			okCancelAlertController.AddAction(UIAlertAction.Create("CONTINUE", UIAlertActionStyle.Default, alert =>
			{
				string s = string.Empty;
				if (IsValidate())
				{
					for (int i = 0; i < SelectedDates.Count; i++)
					{
						s += date.Year + "-" + date.Month + "-" + SelectedDates[i].ToString() + ",";
					}
					s.TrimEnd(',');
					Console.WriteLine(s);

					SubmitAbsentReport(s, txtReason.Text);
				}
			}));
			okCancelAlertController.AddAction(UIAlertAction.Create("NO", UIAlertActionStyle.Cancel, alert => Console.WriteLine("Cancel was clicked")));

			//Present Alert
			PresentViewController(okCancelAlertController, true, null);


		}

		void Calendar_NextMonth (DateTime selectedDate)
		{
			lblYearTitle.Text = selectedDate.ToString("Y").Trim(',');
		}

        void Calendar_PreviousMonth (DateTime selectedDate)
		{
			lblYearTitle.Text = selectedDate.ToString("Y").Trim(',');
		}

		private void GetDropdownList()
		{
			model = new List<StudentModel>();
			try
			{
				BTProgressHUD.Show("Fetching List...");
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						model = WebService.GetStudentByParentId();
					}
				///
				).ContinueWith(
					t =>
					{

						if (model != null)
						{
                        InvokeOnMainThread( () => {
							GetPickerUi.SetupPicker(txtDropdown, model);
    // manipulate UI controls
});
							
							//txtDropdown.Text = model[0].s_fname + " " + model[0].family_name.ToString();
							//txtDropdown.UserInteractionEnabled = true;
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
		private void SubmitAbsentReport(string dates,string reason)
		{
			int responseCode = 0;
			try
			{
				BTProgressHUD.Show("Fetching List...");
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

					responseCode = WebService.SubmitAbsentReport(SelectedStudentId,dates,reason);
					}
				///
				).ContinueWith(
					t =>
					{

						if (responseCode ==200)
						{
						BTProgressHUD.ShowToast("absent has been added successfully.", false, 1000);
							this.DismissViewController(false, null);

						}
						else
						{
						BTProgressHUD.ShowToast("Failed to add absent, Please try again later!.", false, 1000);
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
		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			SelectedStudentId = 0;

		}
    }
}