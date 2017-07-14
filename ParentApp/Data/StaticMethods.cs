using System;
using System.Drawing;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Geolocation;

namespace ParentApp
{
	public class StaticMethods
	{
		public static UIStoryboard MainStoryboard 
		{

			get { return UIStoryboard.FromName("Main", NSBundle.MainBundle); }
		}
		public static UIStoryboard Main_ArabicStoryboard
		{

			get { return UIStoryboard.FromName("Main_Arabic", NSBundle.MainBundle); }
		}


		public static void GetLocation()
		{
			try
			{
				var locator = new Geolocator { DesiredAccuracy = 50 };
				//            new Geolocator (this) { ... }; on Andro
				locator.GetPositionAsync(timeout: 10000).ContinueWith(t =>
						{
							StaticDataModel.Lattitude = t.Result.Latitude;
					StaticDataModel.Longitude = t.Result.Longitude;
							Console.WriteLine("Position Status: {0}", t.Result.Timestamp);
							Console.WriteLine("Position Latitude: {0}", t.Result.Latitude);
							Console.WriteLine("Position Longitude: {0}", t.Result.Longitude);
						}, TaskScheduler.FromCurrentSynchronizationContext());

			}
			catch (Exception ex)
			{

			}
		}
		public static void SetTextFieldLeftIcon(UITextField textfield,string image)
		{
			try
			{
				int padding = 30;
				var type=DeviceType();
				if (type == "ipad")
				{
					 padding = 40;
				}
				else
				{ 
				 padding = 30;
				}

				var imageView = new UIImageView(UIImage.FromBundle(image))
				{
					// Indent it 10 pixels from the left.
					Frame = new RectangleF(10, 0, 20, 20)
				};

				UIView objLeftView = new UIView(new Rectangle(0, 0, 30, 20));
				objLeftView.AddSubview(imageView);

				textfield.LeftViewMode = UITextFieldViewMode.Always;
				textfield.LeftView = objLeftView;

			}
			catch (Exception ex)
			{

			}
		}
		public static void SetPadding(UITextField f, int padding)
		{
			try
			{
				if (StaticDataModel.CurrentLanguage == "en")
				{
					UIView paddingView = new UIView(new RectangleF(0, 0, padding, 20));
					f.LeftView = paddingView;
					f.LeftViewMode = UITextFieldViewMode.Always;
				}
			}
			catch (Exception ex)
			{

			}

		}
		public static bool IsConnectedToInternet()
		{
			try
			{
				if (!Reachability.IsHostReachable("http://google.com"))
				{
					return false;
				}
				else
				{

					return true;
				}
			}
			catch (Exception ex)
			{
				return true;
			}
		}
		public static UIImage GetRoundImage(UIImage im)
		{
			UIImage im1 = null;
			try
			{
				var width = float.Parse( im.Size.Width.ToString());
				var height = float.Parse(im.Size.Height.ToString());
				if (im == null)
					return null;
				UIGraphics.BeginImageContextWithOptions(new SizeF(width / 2, height/ 2), false, 0);
				var height1 = im.Size.Height;
				UIBezierPath.FromRoundedRect(
					new RectangleF(0, 0, width / 2, height / 2),
					im.Size.Width / 2
				).AddClip();
				im.Draw(new RectangleF(0, 0, width / 2, height / 2));
				 im1 = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();
				return im1;
			}
			catch (Exception ex)
			{
				return im1;
			}

		}
		public static string DeviceType()
		{
			if (StaticDataModel.DeviceHeight > 800)
			{
				return "ipad";
			}
			return "iphone";
		}
		public static void ChangeLocalization(string language_code)
		{ 
         try
			{
				NSUserDefaults.StandardUserDefaults.SetValueForKey(NSArray.FromStrings(language_code), new NSString("AppleLanguages"));
				NSUserDefaults.StandardUserDefaults.Synchronize();
			}
			catch (Exception ex)
			{
	
}
		}
	}
}
