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
    [Register ("NavigationMenu")]
    partial class NavigationMenu
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tblMenuList { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (tblMenuList != null) {
                tblMenuList.Dispose ();
                tblMenuList = null;
            }
        }
    }
}