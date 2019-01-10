using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Kinvey;

namespace XamarinKinveyMIC
{
    public partial class MainPage : ContentPage
    {
        private readonly Client KinveyClient;
        private string appKey = "xxx";
        private string appSecret = "xxx";
        private string userName = "xxx";
        private string userPassword = "xxx";
        private string authServiceID = "xxx";

        public MainPage()
        {
            InitializeComponent();
            Client.Builder Builder = new Client.Builder(this.appKey, this.appSecret).setLogger(Console.WriteLine);
            this.KinveyClient = Builder.Build();
            this.KinveyPing();
        }

        private async void KinveyPing()
        {
            try
            {
                PingResponse PingResponse = await this.KinveyClient.PingAsync();
                await DisplayAlert("Kinvey Ping Response", "Kinvey Ping Response: " + PingResponse.kinvey, "OK");
            }
            catch (Exception PingException)
            {
                await DisplayAlert("Kinvey Ping Exception", "Kinvey Ping Exception: Please check logs.", "OK");
                Console.WriteLine("Kinvey Ping Exception: " + PingException.Message);
            }
        }

        public async void KinveyLoginMIC()
        {
            // If there's a User already, please log-out.
            if (Client.SharedClient.ActiveUser != null)
            {
                Client.SharedClient.ActiveUser.Logout();
            }
            await User.LoginWithMIC(this.userName, this.userPassword, this.authServiceID, this.KinveyClient);
            await DisplayAlert("Successfully logged-in!", "Username: " + Client.SharedClient.ActiveUser.UserName, "OK");
        }
    }
}
