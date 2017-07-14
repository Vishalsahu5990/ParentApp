using Foundation;
using System;
using UIKit;
using BigTed;

namespace ParentApp
{
    public partial class Profile : UIViewController
    {
        public Profile (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			SetData();
			PrepareUI();
			imgBack.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
		   {
			   this.DismissModalViewController(false);

		   }));
		}
		private void PrepareUI()
		{ 
		try
			{
				uiviewInnerView.Layer.BorderColor = UIColor.LightGray.CGColor;
				uiviewInnerView.Layer.BorderWidth = 2;
			}
			catch (Exception ex)
			{

			}
		}
		private void SetData()
		{ 
		try
			{
				Login.dictionary = NSUserDefaults.StandardUserDefaults.DictionaryForKey(Login.key);

				string UserName = Login.dictionary["UserName"].ToString();
				string user_email = Login.dictionary["user_email"].ToString();
				string first_name = Login.dictionary["first_name"].ToString();
				string middle_name = Login.dictionary["middle_name"].ToString();
				string family_name = Login.dictionary["family_name"].ToString();
				string contact_number = Login.dictionary["contact_number"].ToString();
				string mobile_number = Login.dictionary["mobile_number"].ToString();
				if (!string.IsNullOrEmpty(UserName))
				{
					txtUsername.Text = UserName;
					txtFirstname.Text = first_name.Replace("\"", string.Empty).Trim();
					txtMiddlename.Text = middle_name.Replace("\"", string.Empty).Trim();
					txtFamilyname.Text = family_name.Replace("\"", string.Empty).Trim();
					txtMobileno.Text = contact_number.Replace("\"", string.Empty).Trim();
					txtContactno.Text = mobile_number.Replace("\"", string.Empty).Trim();
					txtEmail.Text = user_email.Replace("\"", string.Empty).Trim();

					if (string.IsNullOrEmpty(txtMobileno.Text))
					{
						txtMobileno.Text = "N/A";
					}
					if (string.IsNullOrEmpty(txtContactno.Text))
					{
					
txtContactno.Text = "N/A";
					}
				if (string.IsNullOrEmpty(txtEmail.Text))
				{
					
						txtEmail.Text = "N/A";
				}
			}
				else
				{ 
				BTProgressHUD.ShowToast("Problem on fetching details, Please try again later.", false, 10000);

				}

			}
			catch (Exception ex)
			{

			}
		}
    }
}