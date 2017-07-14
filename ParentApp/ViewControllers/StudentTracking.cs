using Foundation;
using System;
using UIKit;
using CoreLocation;
using MapKit;
using System.Threading.Tasks;
using Xamarin.Geolocation;
using System.Drawing;
using CoreGraphics;
using CoreAnimation;
using BigTed;
using System.Threading;
using SDWebImage;

namespace ParentApp
{
    public partial class StudentTracking : UIViewController
    {
		private NSTimer _timer;
		MyMapDelegate mapDel=null;
		public static int indiacator = 0;
		BusTrackingModel model1 = null;
		StudentLocationModel model = null;
        public StudentTracking (IntPtr handle) : base (handle)
        {
			
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			//StaticMethods.GetLocation();
			WebService.GetRealtimeBusLatLong(StaticDataModel.StudentId,StaticDataModel.StudentRouteId);
			imgBack.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
		   {
			   this.DismissModalViewController(false);

		   }));

		}
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			GetStudentLatLong();
			if (_timer == null)
			{
				Timers();
			}


		}
		public override void ViewWillDisappear(Boolean animated)
		{
			if (_timer != null)
				_timer.Invalidate();
		}
		private void Timers()
		{
			_timer = NSTimer.CreateRepeatingScheduledTimer(new TimeSpan(0, 0, 4), delegate
			{
				BeginInvokeOnMainThread(delegate
				{
					//Check network connections here
					Console.WriteLine("working*******");
					GetRealTimeLatLong();
				});
			});
		}
		private void PrepareMap(StudentLocationModel slm)
		{
			
		try
			{
				if(mapview.Annotations!=null)
				mapview.RemoveAnnotations(mapview.Annotations);
				//foreach (var item in mapview.Annotations)
				//{
				//	mapview.RemoveAnnotation(item);
				//}
				    mapview.SetNeedsDisplay();
				
					CLLocationCoordinate2D coordsforStudent = new CLLocationCoordinate2D(slm.student_lat, slm.student_lng);
					MKCoordinateSpan span1 = new MKCoordinateSpan(MilesToLatitudeDegrees(2), MilesToLongitudeDegrees(2, coordsforStudent.Latitude));
					mapview.Region = new MKCoordinateRegion(coordsforStudent, span1);
					

					CLLocationCoordinate2D coordsforSource = new CLLocationCoordinate2D(slm.source_lat, slm.source_lng);
					MKCoordinateSpan span2 = new MKCoordinateSpan(MilesToLatitudeDegrees(2), MilesToLongitudeDegrees(2, coordsforSource.Latitude));
					mapview.Region = new MKCoordinateRegion(coordsforSource, span2);
				    

					CLLocationCoordinate2D coordsfordestinatin = new CLLocationCoordinate2D(slm.destination_lat, slm.destination_lng);
					MKCoordinateSpan span3 = new MKCoordinateSpan(MilesToLatitudeDegrees(2), MilesToLongitudeDegrees(2, coordsfordestinatin.Latitude));
					mapview.Region = new MKCoordinateRegion(coordsfordestinatin, span3);
				    

				// set the map delegate
				mapDel = new MyMapDelegate ();
			    mapview.Delegate = mapDel;

			// add a custom annotation
				mapview.AddAnnotation(new CustomAnnotation(0,StaticDataModel.StudentInfo+"'s Home",coordsforStudent));
				mapview.AddAnnotation(new CustomAnnotation(1,"Source", coordsforSource));
				mapview.AddAnnotation(new CustomAnnotation(2,"Destination", coordsfordestinatin));

				//// add a custom annotationTesting prupose
				//CLLocationCoordinate2D coords1 = new CLLocationCoordinate2D(StaticDataModel.Lattitude, StaticDataModel.Longitude);
				//mapview.AddAnnotation(new CustomAnnotation("Bus",coords1));
			}
			catch (Exception ex)
			{

			}
		}
		public static  double MilesToLatitudeDegrees(double miles)
		{
			double earthRadius = 3960.0; // in miles
			double radiansToDegrees = 180.0 / Math.PI;
			return (miles / earthRadius) * radiansToDegrees;
		}
		public static double MilesToLongitudeDegrees(double miles, double atLatitude)
		{
			double earthRadius = 3960.0; // in miles
			double degreesToRadians = Math.PI / 180.0;
			double radiansToDegrees = 180.0 / Math.PI;
			// derive the earth's radius at that point in latitude
			double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreesToRadians);
			return (miles / radiusAtLatitude) * radiansToDegrees;
		}
		private void GetStudentLatLong()
		{
			model = new StudentLocationModel();
			try
			{
				//BTProgressHUD.Show();
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						
					model = WebService.GetLatLongForStudentTracking(StaticDataModel.StudentId);
					}
				///
				).ContinueWith(
					t =>
					{

						if (model != null)
						{
						
						InvokeOnMainThread(() =>
						{
							PrepareMap(model);

						});

						}
						else
						{

						}
						//BTProgressHUD.Dismiss();

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
			private void GetRealTimeLatLong()
		{
			model1 = new BusTrackingModel();
			try
			{
				//BTProgressHUD.Show();
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
					model1 = WebService.GetRealtimeBusLatLong(StaticDataModel.StudentId,StaticDataModel.StudentRouteId);
					}
				///
				).ContinueWith(
					t =>
					{

						if (model1 != null)
						{
						
						InvokeOnMainThread(() =>
						{
							if (mapview != null)
							{
								var annotation = mapview.Annotations;

								for (int i = 0; i < annotation.Length;i++)
								{ 
                                var title = annotation[i].GetTitle();
								if(title=="studentname")
								{
									mapview.RemoveAnnotation(annotation[i]);

								}
						}
;

							}
							CLLocationCoordinate2D coordsforBus = new CLLocationCoordinate2D(model1.lat, model1.lng);
							//MKCoordinateSpan span3 = new MKCoordinateSpan(MilesToLatitudeDegrees(2), MilesToLongitudeDegrees(2, coordsforBus.Latitude));
							//mapview.Region = new MKCoordinateRegion(coordsforBus, span3);
// get current region
							MKCoordinateRegion region = mapview.Region;
// Update the center

							region.Center = new CLLocationCoordinate2D(model1.lat, model1.lng);
// apply the new region
							mapview.Region = region;

							mapview.AddAnnotation(new CustomAnnotation(5, "studentname", coordsforBus));
							mapview.ZoomEnabled = true;


						});

						}
						else
						{

						}
						//BTProgressHUD.Dismiss();

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
					if (id == 0)
					{
						if (StaticDataModel.BlinkingStatus == 1)
						{
							anView.Image = UIImage.FromFile("greenmarker.png");
						}
						else
						{
							anView.Image = UIImage.FromFile("yellowmarker.png");
						}
						var imageView = new UIImageView(new CGRect(8, 8, 35, 35));
						//imageView.Image = UIImage.FromFile("profilepic.jpg");
						imageView.SetImage(
							url: new NSUrl(StaticDataModel.StudentProfilePic),
											placeholder: UIImage.FromBundle("dummmyprofile_withBorder.png")
											);
						CALayer profileImageCircle = imageView.Layer;
						profileImageCircle.CornerRadius = 17.5f;
						profileImageCircle.MasksToBounds = true;
						imageView.BackgroundColor = UIColor.Clear;
						imageView.Center = imageView.ConvertPointToView(imageView.Center, imageView);
						anView.AddSubview(imageView);

					}
					else if (id == 1)
					{
						anView.Image = UIImage.FromFile("sourcemarker.png");

					}
					else if (id == 2)
					{

						anView.Image = UIImage.FromFile("marker3.png");

					}
					else if (id == 5)
					{ 
					anView.Image = UIImage.FromFile("bus.png");
					}
					anView.CanShowCallout = true;
					anView.Draggable = false;
					//anView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);

				}
				else {

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
