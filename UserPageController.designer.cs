// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SAILIIOS1
{
    [Register ("UserPageController")]
    partial class UserPageController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel WelcomeLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (WelcomeLabel != null) {
                WelcomeLabel.Dispose ();
                WelcomeLabel = null;
            }
        }
    }
}