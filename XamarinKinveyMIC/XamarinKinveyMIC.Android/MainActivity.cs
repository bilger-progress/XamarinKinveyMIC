using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Kinvey;
using Xamarin.Forms;
using XamarinKinveyMIC.Droid;

[assembly: Dependency(typeof(MainActivity))]
namespace XamarinKinveyMIC.Droid
{
    [Activity(Label = "XamarinKinveyMIC", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleInstance, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] { Intent.ActionView },
                  Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
                  DataSchemes =new [] { "myredirecturi" })]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IKinveyMIC
    {
        public void KinveyLoginMIC()
        {
            User.LoginWithMIC("myRedirectURI://", new KinveyMICDelegate<User>()
            {
                onSuccess = (User) => { this.AlertUser(User); },
                onError = (Exception) => { this.AlertException(Exception); },
                onReadyToRender = (url) =>
                {
                    var uri = Android.Net.Uri.Parse(url);
                    var intent = new Intent(Intent.ActionView, uri);
                    Forms.Context.StartActivity(intent);
                }
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            Client.SharedClient.ActiveUser.OnOAuthCallbackReceived(intent);
        }
    }
}