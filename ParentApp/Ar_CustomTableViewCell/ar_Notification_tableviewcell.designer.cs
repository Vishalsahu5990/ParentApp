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
    [Register ("ar_Notification_tableviewcell")]
    partial class ar_Notification_tableviewcell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnDatetime { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDescrioption { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnDatetime != null) {
                btnDatetime.Dispose ();
                btnDatetime = null;
            }

            if (btnTitle != null) {
                btnTitle.Dispose ();
                btnTitle = null;
            }

            if (lblDescrioption != null) {
                lblDescrioption.Dispose ();
                lblDescrioption = null;
            }
        }
    }
}