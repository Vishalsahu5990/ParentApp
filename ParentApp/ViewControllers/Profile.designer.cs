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
    [Register ("Profile")]
    partial class Profile
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgBack { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtContactno { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtFamilyname { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtFirstname { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtMiddlename { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtMobileno { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel txtUsername { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView uiviewInnerView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgBack != null) {
                imgBack.Dispose ();
                imgBack = null;
            }

            if (txtContactno != null) {
                txtContactno.Dispose ();
                txtContactno = null;
            }

            if (txtEmail != null) {
                txtEmail.Dispose ();
                txtEmail = null;
            }

            if (txtFamilyname != null) {
                txtFamilyname.Dispose ();
                txtFamilyname = null;
            }

            if (txtFirstname != null) {
                txtFirstname.Dispose ();
                txtFirstname = null;
            }

            if (txtMiddlename != null) {
                txtMiddlename.Dispose ();
                txtMiddlename = null;
            }

            if (txtMobileno != null) {
                txtMobileno.Dispose ();
                txtMobileno = null;
            }

            if (txtUsername != null) {
                txtUsername.Dispose ();
                txtUsername = null;
            }

            if (uiviewInnerView != null) {
                uiviewInnerView.Dispose ();
                uiviewInnerView = null;
            }
        }
    }
}