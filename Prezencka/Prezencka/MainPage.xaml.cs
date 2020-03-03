using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Prezencka
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                    clock_big.Text = DateTime.Now.ToString("hh:mm:ss"));

                return true;
            });


        }

        private async void ArriveButt(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("PRÍCHOD ZAZNAMENANÝ","Váš príchod bol úspešne zaznamenaný do pracovného výkazu.","", "OK");
            Debug.WriteLine("Answer: " + answer);
        }

        private async void RestButt(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("PRESTÁVKA ZAZNAMENANÁ", "Vaša prestávka bola úspešne zaznamenaná do pracovného výkazu.", "", "OK");
            Debug.WriteLine("Answer: " + answer);
        }

        private async void LeaveButt(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("ODCHOD ZAZNAMENANÝ", "Váš odchod bol úspešne zaznamenaný do pracovného výkazu.", "", "OK");
            Debug.WriteLine("Answer: " + answer);
        }
        
        //----------
        private void OwnTimeChangedArrive(object sender, PropertyChangedEventArgs e)
        {
            Recalculate();
        }

        private void OwnTimeChangedRest(object sender, PropertyChangedEventArgs e)
        {
            Recalculate();
        }

        private void OwnTimeChangedLeave(object sender, PropertyChangedEventArgs e)
        {
            Recalculate();
        }

        private async void Recalculate()
        {
            if (own_time_Arrive.Time > own_time_rest.Time)
            {
                bool answer = await DisplayAlert("CHYBA", "Vami zadaný čas nie je správny.", "", "OK");
                Debug.WriteLine("Answer: " + answer);
            }

            else if (own_time_Arrive.Time > own_time_leave.Time)
            {
                bool answer = await DisplayAlert("CHYBA", "Vami zadaný čas nie je správny.", "", "OK");
                Debug.WriteLine("Answer: " + answer);
            }

            else if (own_time_rest.Time > own_time_leave.Time)
            {
                bool answer = await DisplayAlert("CHYBA", "Vami zadaný čas nie je správny.", "", "OK");
                Debug.WriteLine("Answer: " + answer);
            }

            else
            {
                return;
            }
        }
    }
}
