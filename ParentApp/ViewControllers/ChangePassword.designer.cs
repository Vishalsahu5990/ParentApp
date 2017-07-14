// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ParentApp
{
    [Register ("ChangePassword")]
    partial class ChangePassword
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSubmit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgBack { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtConfirmPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtCurrentPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNewPassword { get; set; }

        [Action ("BtnSubmit_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnSubmit_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnSubmit != null) {
                btnSubmit.Dispose ();
                btnSubmit = null;
            }

            if (imgBack != null) {
                imgBack.Dispose ();
                imgBack = null;
            }

            if (txtConfirmPassword != null) {
                txtConfirmPassword.Dispose ();
                txtConfirmPassword = null;
            }

            if (txtCurrentPassword != null) {
                txtCurrentPassword.Dispose ();
                txtCurrentPassword = null;
            }

            if (txtNewPassword != null) {
                txtNewPassword.Dispose ();
                txtNewPassword = null;
            }
        }
    }
}