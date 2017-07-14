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
    [Register ("StudentTracking")]
    partial class StudentTracking
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgBack { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView mapview { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgBack != null) {
                imgBack.Dispose ();
                imgBack = null;
            }

            if (mapview != null) {
                mapview.Dispose ();
                mapview = null;
            }
        }
    }
}