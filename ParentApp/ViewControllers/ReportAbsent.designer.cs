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
    [Register ("ReportAbsent")]
    partial class ReportAbsent
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView _uiview { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnNext { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnPrevious { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSubmit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgBack { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblYearTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtDropdown { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtReason { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView uiviewCalendar { get; set; }

        [Action ("BtnNext_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnNext_TouchUpInside (UIKit.UIButton sender);

        [Action ("BtnPrevious_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnPrevious_TouchUpInside (UIKit.UIButton sender);

        [Action ("BtnSubmit_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnSubmit_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (_uiview != null) {
                _uiview.Dispose ();
                _uiview = null;
            }

            if (btnNext != null) {
                btnNext.Dispose ();
                btnNext = null;
            }

            if (btnPrevious != null) {
                btnPrevious.Dispose ();
                btnPrevious = null;
            }

            if (btnSubmit != null) {
                btnSubmit.Dispose ();
                btnSubmit = null;
            }

            if (imgBack != null) {
                imgBack.Dispose ();
                imgBack = null;
            }

            if (lblDescription != null) {
                lblDescription.Dispose ();
                lblDescription = null;
            }

            if (lblYearTitle != null) {
                lblYearTitle.Dispose ();
                lblYearTitle = null;
            }

            if (txtDropdown != null) {
                txtDropdown.Dispose ();
                txtDropdown = null;
            }

            if (txtReason != null) {
                txtReason.Dispose ();
                txtReason = null;
            }

            if (uiviewCalendar != null) {
                uiviewCalendar.Dispose ();
                uiviewCalendar = null;
            }
        }
    }
}