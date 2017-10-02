using System;
using System.Collections.Generic;
using System.IO;
using UIKit;
using SAILIIOS;

namespace SAILIIOS1
{
    public partial class ViewController : UIViewController
    {
        private string pathToDatabase;
        private List<User> UserList;
        private User wanttedUser;
        protected ViewController(IntPtr handle) : base(handle)
        {
            UserList = new List<User>();
        }
    			

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			pathToDatabase = Path.Combine(documentsFolder, "Usertest2_db.db");
        }

        partial void LoginButton_TouchUpInside(UIButton sender)
        {
            
            PasswordManager PM = new PasswordManager();
            using (var connection = new SQLite.SQLiteConnection(pathToDatabase))
            {
                var query = connection.Table<User>();
                foreach (User user in query)
                {
                    UserList.Add(user);
                }

                //check if user existed in the database


                if(gothroughDatabaseByUsername(UserList,UsernameText.Text) != null)
                    
                    {
                    User selectedUser = gothroughDatabaseByUsername(UserList, UsernameText.Text);
                        //check if user typed match database account 

                    if (PM.IsPasswordMatch(PasswordText.Text, selectedUser.OwnerSalt, selectedUser.HashedPassword))
                        {
                            UserPageController UPC = this.Storyboard.InstantiateViewController("userpagecontroller") as UserPageController;
                            NavigationController.PushViewController(UPC, true);
                            UPC.inAccountName = "Welcome" + UsernameText.Text;
                        }

                        //if account existed , password not match
                        else
                        {
                            var alert = UIAlertController.Create("Password Incorrect ", "Please input correct password", UIAlertControllerStyle.Alert);
                            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                            PresentViewController(alert, true, null);
                        }
                    }
                    else
                    {
                        var alert = UIAlertController.Create("Username does not exist", "Please input correct Username and password to login", UIAlertControllerStyle.Alert);
                        alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                        PresentViewController(alert, true, null);
                    }
            }
        }

        partial void ViewDatabaseButton_TouchUpInside(UIButton sender)
        {
            using (var connection = new SQLite.SQLiteConnection(pathToDatabase))
            {
                var query = connection.Table<User>();
                foreach(User user in query)
                {
                    UserList.Add(user);
                }
            }
        }

        public static User gothroughDatabaseByUsername(List<User>list , string wanttedname)
        {
            User wanttedUser = new User();
            foreach(User user in list)
            {
                if (user.Username.Equals(wanttedname)) 
                {
                  wanttedUser = user;  
                }

            }
            return wanttedUser;
        }



        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
