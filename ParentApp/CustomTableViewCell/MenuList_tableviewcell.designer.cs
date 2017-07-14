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
    [Register ("MenuList_tableviewcell")]
    partial class MenuList_tableviewcell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnBadgeCount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgIcon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnBadgeCount != null) {
                btnBadgeCount.Dispose ();
                btnBadgeCount = null;
            }

            if (imgIcon != null) {
                imgIcon.Dispose ();
                imgIcon = null;
            }

            if (lblText != null) {
                lblText.Dispose ();
                lblText = null;
            }
        }
    }
}