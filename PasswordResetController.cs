using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.IO;
using SAILIIOS;

namespace SAILIIOS1
{
    public partial class PasswordResetController : UIViewController
    {
        private string pathToDatabase;
        public User enteredUser;
        public string salt;
        public PasswordResetController (IntPtr handle) : base (handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			pathToDatabase = Path.Combine(documentsFolder, "Usertest2_db.db");
            salt = enteredUser.OwnerSalt;
        }

        partial void ChangePasswordButton_TouchUpInside(UIButton sender)
        {
            
            if(NewPasswordText.Text.Equals(ConfirmPwdText.Text))
            {
                if(SignUpPageController.CheckPassword(NewPasswordText.Text))
                {
                    PasswordManager PM = new PasswordManager();

                    enteredUser.HashedPassword = PM.GeneratePasswordHash(NewPasswordText.Text, out salt);
                    using (var connection = new SQLite.SQLiteConnection(pathToDatabase))
                    {
                        var query = connection.Update(enteredUser);

                    }

                    NavigationController.PopToRootViewController(true);
					var alert = UIAlertController.Create("Successful", "Password Updated Successfully", UIAlertControllerStyle.Alert);
					alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
					PresentViewController(alert, true, null);
                }
                else 
                {
					var alert = UIAlertController.Create("Password format Incorrect", "Password should contain number , upper and lower case", UIAlertControllerStyle.Alert);
					alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
					PresentViewController(alert, true, null);
                }
            }
            else
            {
				var alert = UIAlertController.Create("Password not match", "Please make sure the newpassword and confirmpasswrod is the same", UIAlertControllerStyle.Alert);
				alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
				PresentViewController(alert, true, null);
            }
        }
    }
}