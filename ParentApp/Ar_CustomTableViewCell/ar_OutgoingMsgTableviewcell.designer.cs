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
    [Register ("ar_OutgoingMsgTableviewcell")]
    partial class ar_OutgoingMsgTableviewcell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnTickMark { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbl1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbl2 { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnTickMark != null) {
                btnTickMark.Dispose ();
                btnTickMark = null;
            }

            if (lbl1 != null) {
                lbl1.Dispose ();
                lbl1 = null;
            }

            if (lbl2 != null) {
                lbl2.Dispose ();
                lbl2 = null;
            }
        }
    }
}