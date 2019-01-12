using System;
using Foundation;
using UIKit;
using Kinvey;
using Xamarin.Forms;
using XamarinKinveyMIC.iOS;

[assembly: Dependency (typeof (AppDelegate))]
namespace XamarinKinveyMIC.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IKinveyMIC
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public void KinveyLoginMIC()
        {
            User.LoginWithMIC("myRedirectURI://", new KinveyMICDelegate<User>()
            {
                onSuccess = (User) => { this.AlertUser(User); },
                onError = (Exception) => { this.AlertException(Exception); },
                onReadyToRender = (url) => { UIApplication.SharedApplication.OpenUrl(new NSUrl(url)); }
            });
        }

        private void AlertUser(User User)
        {
            Console.WriteLine("User: " + User.Id);
        }

        private void AlertException(Exception Exception)
        {
            Console.WriteLine("Something went wrong: " + Exception.Message);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return Client.SharedClient.ActiveUser.OnOAuthCallbackReceived(url);
        }
    }
}