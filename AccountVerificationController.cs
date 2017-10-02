using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.IO;

namespace SAILIIOS1
{
    public partial class AccountVerificationController : UIViewController
    {
        private string pathToData;
        private List<User> Users;
        public AccountVerificationController (IntPtr handle) : base (handle)
        {
            Users = new List<User>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            pathToData = Path.Combine(documentsFolder, "Usertest2_db.db");

        }

        partial void VerifyButton_TouchUpInside(UIButton sender)
        {
            using(var connection = new SQLite.SQLiteConnection(pathToData))
            {
                var query = connection.Table<User>();
                foreach(User user in query)
                {
                    if (UsernameText.Text.Equals(user.Username))
                    {
                        if(EmailText.Text.Equals(user.Email))
                        {
                            
                            PasswordResetController PRC = this.Storyboard.InstantiateViewController("prc") as PasswordResetController;
                            PRC.enteredUser = user;
                            NavigationController.PushViewController(PRC, true);
                        }
                        else
                        {
							var alert = UIAlertController.Create("Email Incorrect", "Please Input correct email of your account", UIAlertControllerStyle.Alert);
							alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
							PresentViewController(alert, true, null);
                        }
                    }
                    else if(!UsernameText.Text.Equals(user.Username))
                    {
						var alert = UIAlertController.Create("Username Incorrect", "Please Input correct Username of your account", UIAlertControllerStyle.Alert);
						alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
						PresentViewController(alert, true, null);
                    }
                }
            }
        }
    }
}