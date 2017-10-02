using Foundation;
using System;
using UIKit;

namespace SAILIIOS1
{
    public partial class UserPageController : UIViewController
    {
        public string inAccountName { set; get; }
        public UserPageController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            WelcomeLabel.Text = inAccountName;
        }





    }
}