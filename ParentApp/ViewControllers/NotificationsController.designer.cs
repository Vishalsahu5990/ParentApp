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
    [Register ("NotificationsController")]
    partial class NotificationsController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnOk { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgBack { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tblNotification { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView uiviewPopup { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnOk != null) {
                btnOk.Dispose ();
                btnOk = null;
            }

            if (imgBack != null) {
                imgBack.Dispose ();
                imgBack = null;
            }

            if (lblDescription != null) {
                lblDescription.Dispose ();
                lblDescription = null;
            }

            if (lblTitle != null) {
                lblTitle.Dispose ();
                lblTitle = null;
            }

            if (tblNotification != null) {
                tblNotification.Dispose ();
                tblNotification = null;
            }

            if (uiviewPopup != null) {
                uiviewPopup.Dispose ();
                uiviewPopup = null;
            }
        }
    }
}