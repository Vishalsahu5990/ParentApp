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
    [Register ("ChatWithSchool")]
    partial class ChatWithSchool
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSend { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgBack { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgSchoolLogo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgTyping { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton lblSchoolName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tblConversation { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtWritehere { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView uiviewTextwithButton { get; set; }

        [Action ("BtnSend_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnSend_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnSend != null) {
                btnSend.Dispose ();
                btnSend = null;
            }

            if (imgBack != null) {
                imgBack.Dispose ();
                imgBack = null;
            }

            if (imgSchoolLogo != null) {
                imgSchoolLogo.Dispose ();
                imgSchoolLogo = null;
            }

            if (imgTyping != null) {
                imgTyping.Dispose ();
                imgTyping = null;
            }

            if (lblSchoolName != null) {
                lblSchoolName.Dispose ();
                lblSchoolName = null;
            }

            if (tblConversation != null) {
                tblConversation.Dispose ();
                tblConversation = null;
            }

            if (txtWritehere != null) {
                txtWritehere.Dispose ();
                txtWritehere = null;
            }

            if (uiviewTextwithButton != null) {
                uiviewTextwithButton.Dispose ();
                uiviewTextwithButton = null;
            }
        }
    }
}