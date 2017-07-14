using Foundation;
using System;
using UIKit;
using System.Drawing;
using Factorymind.Components;
using WebKit;
using System.IO;
using SDWebImage;
using CoreLocation;
using MapKit;
using System.Threading.Tasks;
using BigTed;

namespace ParentApp 
{
    public partial class StudentReport : UIViewController
    {
		CustomCalendar calendar;
		public int schoolid = 0;
		static DateTime CurrentDate = DateTime.Now;
        public StudentReport (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			PrepareDatePicker(txtFrom);
			PrepareDatePicker(txtTo);
			//NavigationController.SetNavigationBarHidden(true, true);
			PrepareUI();

			uiviewStudentInfo.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
		   {
				 OnSwipe_Left();
			  
		   }));
			uiviewAttandenceReport.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
		   {
			    OnSwipe_Right();
		   }));

			imgBack.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
		   {
			   this.DismissModalViewController(false);

		   }));
			StudentList_tableviewcell.ViewDetailsClicked += (object sender, EventArgs e) =>
			 {
				 StudentTracking home = this.Storyboard.InstantiateViewController("StudentTracking") as StudentTracking;
				 if (home != null)
				 {
					 this.PresentModalViewController(home, true);

				 }
			 };
			AttendanceReportModule();
			StudentInfoModule();
		}
		public override void ViewDidLayoutSubviews()
		{
			this.scrollviewAttendanceReport.ContentSize = uiviewAttendanceReport.Bounds.Size;
			this.scrollviewDetailedInfo.ContentSize = uiviewDetailedInfo.Bounds.Size;     

		}

        partial void BtnSearch_TouchUpInside(UIButton sender)
		{
			if (!string.IsNullOrEmpty(txtFrom.Text) && !string.IsNullOrEmpty(txtFrom.Text) )
			{
				GetabsentPresent(txtFrom.Text,txtTo.Text);
			}
		}

		partial void BtnNext_TouchUpInside(UIButton sender)
		{
			calendar.OnArrowTouchUp(sender);

		}

		partial void BtnPrevious_TouchUpInside(UIButton sender)
		{
			calendar.OnArrowTouchUp(sender);
		}

		private void PrepareGestureRecognizerEvents()
		{
			try
			{
				
				UISwipeGestureRecognizer recognizer3 = new UISwipeGestureRecognizer(OnSwipe_Left);
				recognizer3.Direction = UISwipeGestureRecognizerDirection.Left;
				this.uiviewOne.AddGestureRecognizer(recognizer3);

				UISwipeGestureRecognizer recognizer4 = new UISwipeGestureRecognizer(OnSwipe_Right);
				recognizer4.Direction = UISwipeGestureRecognizerDirection.Right;
				this.uiviewtwo.AddGestureRecognizer(recognizer4);

			}
			catch (Exception ex)
			{

			}
		}
		private void OnSwipe_Left()
		{
			
		    uiviewAttandenceReport.BackgroundColor = UIColor.White;
			uiviewStudentInfo.BackgroundColor = new UIColor(red: 0.018f, green: 0.541f, blue: 0.593f, alpha: 1.0f);
			lblAttendanceReport.TextColor = UIColor.Black;
			lblStudentInfo.TextColor = UIColor.White;
			uiviewtwo.Hidden = false;
			uiviewOne.Hidden = true;
				
		}
		private void OnSwipe_Right()
		{
			
			uiviewStudentInfo.BackgroundColor = UIColor.White;
			uiviewAttandenceReport.BackgroundColor = new UIColor(red: 0.018f, green: 0.541f, blue: 0.593f, alpha: 1.0f);
			lblStudentInfo.TextColor = UIColor.Black;
			lblAttendanceReport.TextColor = UIColor.White;
			uiviewOne.Hidden = false;
			uiviewtwo.Hidden = true;
		}
		private void PrepareUI()
		{
			try
			{
				PrepareGestureRecognizerEvents();
				uiviewtwo.Hidden = true;
				uiviewStudentInfo.BackgroundColor = UIColor.White;
				lblAttendanceReport.TextColor = UIColor.White;

				StaticMethods.SetPadding(txtFrom, 30);
				StaticMethods.SetPadding(txtTo, 30);

				var type = StaticMethods.DeviceType();
				if (type != "ipad")
				{
				uiviewCalender.Frame = new CoreGraphics.CGRect(uiviewCalender.Frame.X,
															   uiviewCalender.Frame.Y + 50,
															   uiviewCalender.Frame.Width,
															   uiviewCalender.Frame.Height);	
				}



			}
			catch (Exception ex)
			{

			}
		}
		private void CreateCalanderView()
		{ 
		try
			{
				
				calendar = new CustomCalendar(new CoreGraphics.CGPoint(0, 20), DateTime.Now, new DateTime(1984, 8, 15), new DateTime(2020, 8, 15));

				this.uiviewCalender.AddSubview(calendar);

				calendar.PreviousMonth += (DateTime selectedDate) =>
				 {
					 
					 if (CurrentDate.Month == selectedDate.Month)
					 {
						 tp.Text = CustomCalendar.Present.ToString();
						 th.Text = CustomCalendar.Holidays.ToString();
						 var absent2 = CustomCalendar.DaysInMonth - (CustomCalendar.Present + CustomCalendar.Holidays + CustomCalendar.Sunday);
						 if (CustomCalendar.FutureDays > absent2)
							 absent2 = CustomCalendar.FutureDays - absent2;
						 else
							 absent2 = absent2 - CustomCalendar.FutureDays;
						 ta.Text = absent2.ToString();
					 }
					 else if (CurrentDate.Month > selectedDate.Month)
					 {
						 th.Text = CustomCalendar.Holidays.ToString();
						 var absent1 = CustomCalendar.DaysInMonth - (CustomCalendar.Present + CustomCalendar.Holidays + CustomCalendar.Sunday);
						 ta.Text = absent1.ToString();
						tp.Text = CustomCalendar.Present.ToString();
					 }
					 else
					 { 
					     th.Text = CustomCalendar.Holidays.ToString();
						 if (selectedDate.Month <= CurrentDate.Month)
						 { 
						     var absent1 = CustomCalendar.DaysInMonth - (CustomCalendar.Present + CustomCalendar.Holidays + CustomCalendar.Sunday);
							 ta.Text = absent1.ToString();
						 }

						tp.Text = CustomCalendar.Present.ToString();
					 }
 

				 lblYearTitle.Text = selectedDate.ToString("Y").Trim(',');
					  
				 };
				calendar.NextMonth += (DateTime selectedDate) =>
				 {
					 if (CurrentDate.Month < selectedDate.Month)
					 {
						 th.Text = CustomCalendar.Holidays.ToString();

						 ta.Text = "0";
						 tp.Text = "0";

					 }
					 else if (CurrentDate.Month == selectedDate.Month)
					 {
						tp.Text = CustomCalendar.Present.ToString();
						 th.Text = CustomCalendar.Holidays.ToString();
						 var absent2 = CustomCalendar.DaysInMonth - (CustomCalendar.Present + CustomCalendar.Holidays + CustomCalendar.Sunday);
						 if (CustomCalendar.FutureDays > absent2)
							 absent2 = CustomCalendar.FutureDays - absent2;
						 else
							 absent2 = absent2 - CustomCalendar.FutureDays;
						 ta.Text = absent2.ToString();
					 }
					 else
					 { 
						 tp.Text = CustomCalendar.Present.ToString();
					     th.Text = CustomCalendar.Holidays.ToString();
						 var absent1 = CustomCalendar.DaysInMonth - (CustomCalendar.Present + CustomCalendar.Holidays + CustomCalendar.Sunday);
						 ta.Text = absent1.ToString();
					 }

				lblYearTitle.Text = selectedDate.ToString("Y").Trim(',');

					};

				tp.Text = CustomCalendar.Present.ToString();
				th.Text = CustomCalendar.Holidays.ToString();
				var absent = CustomCalendar.DaysInMonth - (CustomCalendar.Present + CustomCalendar.Holidays+ CustomCalendar.Sunday);

				Console.WriteLine(CustomCalendar.FutureDays.ToString());
				if (CustomCalendar.FutureDays > absent)
					absent = CustomCalendar.FutureDays - absent;
				else
					absent = absent-CustomCalendar.FutureDays;
				ta.Text = absent.ToString();
			}
			catch (Exception ex)
			{

			}
		}
		private void PrepareMap(double lat,double lng)
		{
			try
			{
				CLLocationCoordinate2D coordsfordestinatin = new CLLocationCoordinate2D(lat, lng);
				MKCoordinateSpan span3 = new MKCoordinateSpan(StudentTracking. MilesToLatitudeDegrees(2), StudentTracking.MilesToLongitudeDegrees(2, coordsfordestinatin.Latitude));
				mapview.Region = new MKCoordinateRegion(coordsfordestinatin, span3);

				// set the map delegate
				var mapDel = new MyMapDelegate();
				mapview.Delegate = mapDel;
				// add a custom annotation
				mapview.AddAnnotation(new CustomAnnotation(0, StaticDataModel.StudentInfo + "'student location", coordsfordestinatin));


			}
			catch (Exception ex)
			{

			}


		}
		private void AttendanceReportModule()
		{
			try
			{
				lblYearTitle.Text = DateTime.Now.ToString("Y").Trim(',');
				GetAttendance();

			}
			catch (Exception ex)
			{

			}


		}
		private void GetabsentPresent(string from,string to)
		{
			string response = string.Empty;
			try
			{
				BTProgressHUD.Show("Loading...");
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

					response= WebService.GetStudentPresentAbsent(StaticDataModel.StudentId, schoolid, from,to);
					}
				///
				).ContinueWith(
					t =>
					{
					if (!string.IsNullOrEmpty(response))
						{
							var data = response.Split(',');
							tp1.Text = data[0];
						ta1.Text = data[1];
						th1.Text = data[2];
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
		private void GetAttendance()
		{
			
			try
			{
				BTProgressHUD.Show("Loading...");
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						WebService.GetStudentAttendance(StaticDataModel.StudentId);
					}
				///
				).ContinueWith(
					t =>
					{

					if (WebService.Holidaylist!=null)
						{
							
						CreateCalanderView();
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
		private void StudentInfoModule()
		{
			try
			{
				GetStudentDetails();

			}
			catch (Exception ex)
			{

			}


		}
		private void GetStudentDetails()
		{
			try
			{
				if (WebService.list != null)
				{
					var model = WebService.list;
					var count=WebService.list.Count;
					for (int i = 0; i < count; i++)
					{
						if (StaticDataModel.StudentId == Convert.ToInt32(model[i].student_id))
						{
							
							imgStudentImage.SetImage(
								url: new NSUrl(WebService.ImageUrl +model[i].s_image_path),
											placeholder: UIImage.FromBundle("dummy_StudentProile.png")
											);


							imgStudentImage1.SetImage(
								url: new NSUrl(WebService.ImageUrl +model[i].s_image_path),
											placeholder: UIImage.FromBundle("dummy_StudentProile.png")
											);

							lblStudentname.Text = model[i].s_fname + " " + model[i].family_name;
							lblStudentName.Text = model[i].s_fname + " " + model[i].family_name;
							txttFirstName.Text = model[i].s_fname;
							txtFatherName.Text = model[i].father_name;
							txtGrandname.Text = model[i].grand_name;
							txtFamilyname.Text = model[i].family_name;
							txtGender.Text = model[i].gender;
							txtClass.Text = model[i].student_class;
							txtBirthdate.Text = model[i].dob;
							txtNationality.Text = model[i].nationality;
							txtBloodType.Text = model[i].blood_type;
							schoolid =Convert.ToInt32( model[i].s_school_id);
							PrepareMap(Convert.ToDouble( model[i].student_lat),Convert.ToDouble(model[i].student_lng));
							break;
						}
					}
				}

			}
			catch (Exception ex)
			{

			}


		}

		public void PrepareDatePicker(UITextField textfield)
		{ 
		try
			{
				
				UIDatePicker datepicker = new UIDatePicker();
				datepicker.Mode = UIDatePickerMode.Date;

				// Setup the toolbar
				UIToolbar toolbar = new UIToolbar();
				toolbar.BarStyle = UIBarStyle.BlackOpaque;
				toolbar.BackgroundColor = new UIColor(red: 0f, green: 0.687f, blue: 0.495f, alpha: 1.0f);
				toolbar.Translucent = true;
				toolbar.SizeToFit();

				// Create a 'done' button for the toolbar and add it to the toolbar
				UIBarButtonItem doneButton = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done,
																 (s, e) =>
																 {

																	 

																	 textfield.ResignFirstResponder();
																 });
				doneButton.TintColor = UIColor.White;
				toolbar.SetItems(new UIBarButtonItem[] { doneButton }, true);

				// Tell the textbox to use the picker for input
				textfield.InputView = datepicker;
				StaticMethods.SetPadding(textfield, 35);
				// Display the toolbar over the pickers
				textfield.InputAccessoryView = toolbar;
				datepicker.ValueChanged += (sender, e) =>
				 { 
					var date = Convert.ToDateTime((sender as UIDatePicker).Date.ToString()).ToString("yyyy-MM-dd");
					 textfield.Text = date.ToString();
				};
			}
			catch (Exception ex)
			{

			}
		}
		class MyMapDelegate : MKMapViewDelegate
		{
			string pId = "PinAnnotation";
			string mId = "MonkeyAnnotation";

			public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
			{
				MKAnnotationView anView;

				if (annotation is MKUserLocation)
					return null;

				if (annotation is CustomAnnotation)
				{
					var id = ((CustomAnnotation)annotation).Id;
					// show monkey annotation
					anView = mapView.DequeueReusableAnnotation(mId);

					if (anView == null)
						anView = new MKAnnotationView(annotation, mId);

					anView.Image = UIImage.FromFile("marker2.png");

					anView.CanShowCallout = true;
					anView.Draggable = false;
					//anView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);

				}
				else
				{

					// show pin annotation
					anView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(pId);

					if (anView == null)
						anView = new MKPinAnnotationView(annotation, pId);

					((MKPinAnnotationView)anView).PinColor = MKPinAnnotationColor.Red;
					anView.CanShowCallout = true;
				}

				return anView;
			}

			public override void CalloutAccessoryControlTapped(MKMapView mapView, MKAnnotationView view, UIControl control)
			{
				var monkeyAn = view.Annotation as CustomAnnotation;

				if (monkeyAn != null)
				{
					var alert = new UIAlertView("Monkey Annotation", monkeyAn.Title, null, "OK");
					alert.Show();
				}
			}

			public override MKOverlayView GetViewForOverlay(MKMapView mapView, IMKOverlay overlay)
			{
				var circleOverlay = overlay as MKCircle;
				var circleView = new MKCircleView(circleOverlay);
				circleView.FillColor = UIColor.Red;
				circleView.Alpha = 0.4f;
				return circleView;
			}
		}
    }
}