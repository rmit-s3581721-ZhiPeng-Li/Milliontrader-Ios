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
    [Register ("AccountVerificationController")]
    partial class AccountVerificationController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField EmailText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField UsernameText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton verifyButton { get; set; }

        [Action ("VerifyButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void VerifyButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (EmailText != null) {
                EmailText.Dispose ();
                EmailText = null;
            }

            if (UsernameText != null) {
                UsernameText.Dispose ();
                UsernameText = null;
            }

            if (verifyButton != null) {
                verifyButton.Dispose ();
                verifyButton = null;
            }
        }
    }
}