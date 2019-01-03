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
        Client KinveyClient = null;
        public MainPage()
        {
            InitializeComponent();
            Client.Builder builder = new Client.Builder("xxx", "xxx").SetInstanceID("xxx");
            this.KinveyClient = builder.Build();
            this.KinveyPing();
        }

        private async void KinveyPing () 
        {
            try
            {
                PingResponse Response = await this.KinveyClient.PingAsync();
                Console.WriteLine("Kinvey Ping Response: " + Response.kinvey);
            }
            catch (Exception KinveyPingException)
            {
                Console.WriteLine("Kinvey Ping Exception: " + KinveyPingException.Message);
            }
        }
    }
}
