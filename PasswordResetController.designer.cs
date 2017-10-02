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
    [Register ("PasswordResetController")]
    partial class PasswordResetController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ChangePasswordButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ConfirmPasswordText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ConfirmPwdText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NewPasswordText { get; set; }

        [Action ("ChangePasswordButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ChangePasswordButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ChangePasswordButton != null) {
                ChangePasswordButton.Dispose ();
                ChangePasswordButton = null;
            }

            if (ConfirmPasswordText != null) {
                ConfirmPasswordText.Dispose ();
                ConfirmPasswordText = null;
            }

            if (ConfirmPwdText != null) {
                ConfirmPwdText.Dispose ();
                ConfirmPwdText = null;
            }

            if (NewPasswordText != null) {
                NewPasswordText.Dispose ();
                NewPasswordText = null;
            }
        }
    }
}