// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace ParentApp
{
    [Register ("ipad_StudentList_tableviewcell")]
    partial class ipad_StudentList_tableviewcell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCall { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnViewDetails { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton imgCar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgProfile { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDriverName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblStudentName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCall != null) {
                btnCall.Dispose ();
                btnCall = null;
            }

            if (btnCar != null) {
                btnCar.Dispose ();
                btnCar = null;
            }

            if (btnViewDetails != null) {
                btnViewDetails.Dispose ();
                btnViewDetails = null;
            }

            if (imgCar != null) {
                imgCar.Dispose ();
                imgCar = null;
            }

            if (imgProfile != null) {
                imgProfile.Dispose ();
                imgProfile = null;
            }

            if (lblDriverName != null) {
                lblDriverName.Dispose ();
                lblDriverName = null;
            }

            if (lblStudentName != null) {
                lblStudentName.Dispose ();
                lblStudentName = null;
            }
        }
    }
}