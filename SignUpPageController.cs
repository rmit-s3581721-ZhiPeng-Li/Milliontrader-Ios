using Foundation;
using System;
using UIKit;
using System.IO;
using SQLite;
using SAILIIOS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SAILIIOS1
{
    public partial class SignUpPageController : UIViewController
    {
        private string pathToDatabase;
        private List<User> Users;

        public SignUpPageController (IntPtr handle) : base (handle)
        {
            Users = new List<User>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            pathToDatabase = Path.Combine(documentsFolder, "Usertest2_db.db");

            using (var connection = new SQLite.SQLiteConnection(pathToDatabase))
            {
                connection.CreateTable<User>();
            }


        }

        partial void SignUpButton_TouchUpInside(UIButton sender)
        {

            using (var connection = new SQLite.SQLiteConnection(pathToDatabase))
            {
                /*
                check if password match
                */
                //if password match
                if((PasswordText.Text).Equals(ConfirmPasswordText.Text))
                {
                    //check if entered Username existed
                    if(!isAccountExisted(UsernameText.Text,pathToDatabase))
                    {

						/*
						Hash and salt the password
						*/

						PasswordManager PM = new PasswordManager();
						string salt = SaltGenerator.GetSaltString();
						string HashedPwd = PM.GeneratePasswordHash(PasswordText.Text, out salt);
						User newuser = new User();
						newuser.Username = UsernameText.Text;
						newuser.HashedPassword = HashedPwd;
                        newuser.OwnerSalt = salt;
						newuser.Email = EmailText.Text;

                        /*
                         * Check if User type valid
                         */
                        //Username should more than 5 charactors
                        if (newuser.Username.Length > 5)
                        {

                            //Password should contain number and upper & lower case
                            if(CheckPassword(PasswordText.Text))
                            {

                                //Email should in the correct format
                                if (CheckEmailValidation(newuser.Email))
                                {
                                    
                                    connection.Insert(newuser);
									//After login successfully , auto go to the userpage controller
									
									UserPageController UPC = this.Storyboard.InstantiateViewController("userpagecontroller") as UserPageController;
									NavigationController.PushViewController(UPC, true);
									UPC.inAccountName = "Welcome" + UsernameText.Text;
									var alert = UIAlertController.Create("Successful", "You have been signed up ", UIAlertControllerStyle.Alert);
									alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
									PresentViewController(alert, true, null);
                                }
                                else
                                {
                                    var alert = UIAlertController.Create("Email format not correct", "Please type a valid email address", UIAlertControllerStyle.Alert);
                                    alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                                    PresentViewController(alert, true, null);
                                }

                            }
                            else
                            {
								var alert = UIAlertController.Create("Password format incorrect", "Password should contain number , upper and lower case", UIAlertControllerStyle.Alert);
								alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
								PresentViewController(alert, true, null);
                            }
                        }
                        else
                        {
							var alert = UIAlertController.Create("Username format incorrect", "Username should have more than 5 charactor", UIAlertControllerStyle.Alert);
							alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
							PresentViewController(alert, true, null);
                        }
					}
                    /*
                     * if User existed 
                     */
                    else
                    {
						var alert = UIAlertController.Create("Username already used", "Please use a new one", UIAlertControllerStyle.Alert);
						alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
						PresentViewController(alert, true, null);
                    }
					
                }

                //password not match
                else
                {
					var alert = UIAlertController.Create("Password not match", "Please make sure ConfirmPassword match the password", UIAlertControllerStyle.Alert);
					alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
					PresentViewController(alert, true, null); 
                }

            }
        }

        /*
         * Check whether entered username existed in the Database
         */

        public static Boolean isAccountExisted(string Username,string databaselocation)
        {
            Boolean exist = false;
            using (var connection = new SQLite.SQLiteConnection(databaselocation))
            {
                var query = connection.Table<User>();
                foreach(User user in query)
                {
                    if(user.Username.Equals(Username))
                    {
                        exist = true;
                    }
                }
            }
            return exist;
        }

        //check Email Validation
        public static Boolean CheckEmailValidation(String email)
        {
            bool EmailisValid = false; 
            try
            {
                var mailAddress = new MailAddress(email);
                EmailisValid = true;
                return EmailisValid;
            }
            catch(FormatException)
            {
                return EmailisValid;
            }
            
        }

        //Check if password follow the rule
        public static bool CheckPassword(string password)
        {
            bool PasswordValid = false;
            string pattern = "(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z])";
            Match match = Regex.Match(password, pattern);
            if(match.Success)
            {
                PasswordValid = true;
            }
            return PasswordValid;
        }

		//Check if Username follow the rule
		//public static bool CheckUsername(string username)
		//{
		//	bool PasswordValid = false;
  //          var regex = new Regex("^[A-Z a-z]$", RegexOptions.Compiled);
  //          if (regex.IsMatch(username))
		//	{
		//		PasswordValid = true;
		//	}
		//	return PasswordValid;
		//}
    }
    public class User 
    {
        [Required]
        [PrimaryKey]
        [StringLength(30,ErrorMessage =  "The {0} must be at leaset {2} characters long",MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string Username { set; get; }

        [Required]
        [StringLength(30,ErrorMessage =  "The Password must at least 8 characters " , MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string HashedPassword { set; get; }

        [Required]
        public string OwnerSalt { set; get; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { set; get; }

        public User(){
           }
    }
}