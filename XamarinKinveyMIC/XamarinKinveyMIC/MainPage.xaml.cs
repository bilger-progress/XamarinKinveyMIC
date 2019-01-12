using System;
using Xamarin.Forms;
using Kinvey;

namespace XamarinKinveyMIC
{
    public partial class MainPage : ContentPage
    {
        private readonly Client KinveyClient;
        private string appKey = "xxx";
        private string appSecret = "xxx";

        public MainPage()
        {
            InitializeComponent();
            Client.Builder Builder = new Client.Builder(this.appKey, this.appSecret);
            this.KinveyClient = Builder.Build();
            this.KinveyPing();
        }

        private async void KinveyPing()
        {
            try
            {
                PingResponse PingResponse = await this.KinveyClient.PingAsync();
                Console.WriteLine("Kinvey Ping Response: " + PingResponse.kinvey);
            }
            catch (Exception PingException)
            {
                Console.WriteLine("Kinvey Ping Exception: " + PingException.Message);
            }
        }

        public void KinveyLoginMIC()
        {
            // Logout in case there's already logged-in User.
            if(Client.SharedClient.ActiveUser != null) 
            {
                Client.SharedClient.ActiveUser.Logout();
            }
            DependencyService.Get<IKinveyMIC>().KinveyLoginMIC();
        }
    }
}
