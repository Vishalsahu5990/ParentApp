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
    [Register ("StudentList")]
    partial class StudentList
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnArabic { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnEnglish { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSave { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgMenu { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgSchoolLogo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton lblSchoolName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tblStudentList { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView uiview { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView uiviewLanguageBorder { get; set; }

        [Action ("Btn_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Btn_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btn != null) {
                btn.Dispose ();
                btn = null;
            }

            if (btnArabic != null) {
                btnArabic.Dispose ();
                btnArabic = null;
            }

            if (btnCancel != null) {
                btnCancel.Dispose ();
                btnCancel = null;
            }

            if (btnEnglish != null) {
                btnEnglish.Dispose ();
                btnEnglish = null;
            }

            if (btnSave != null) {
                btnSave.Dispose ();
                btnSave = null;
            }

            if (imgMenu != null) {
                imgMenu.Dispose ();
                imgMenu = null;
            }

            if (imgSchoolLogo != null) {
                imgSchoolLogo.Dispose ();
                imgSchoolLogo = null;
            }

            if (lblSchoolName != null) {
                lblSchoolName.Dispose ();
                lblSchoolName = null;
            }

            if (tblStudentList != null) {
                tblStudentList.Dispose ();
                tblStudentList = null;
            }

            if (uiview != null) {
                uiview.Dispose ();
                uiview = null;
            }

            if (uiviewLanguageBorder != null) {
                uiviewLanguageBorder.Dispose ();
                uiviewLanguageBorder = null;
            }
        }
    }
}