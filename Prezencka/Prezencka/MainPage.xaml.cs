using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Prezencka
{
    public partial class MainPage
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
            await DisplayAlert("PRÍCHOD ZAZNAMENANÝ","Váš príchod bol úspešne zaznamenaný do pracovného výkazu.", "OK");
        }

        private async void RestButt(object sender, EventArgs e)
        {
            await DisplayAlert("PRESTÁVKA ZAZNAMENANÁ", "Vaša prestávka bola úspešne zaznamenaná do pracovného výkazu.", "OK");
        }

        private async void LeaveButt(object sender, EventArgs e)
        {
            await DisplayAlert("ODCHOD ZAZNAMENANÝ", "Váš odchod bol úspešne zaznamenaný do pracovného výkazu.", "OK");
        }
        
        private async void OnConfirm(object sender, EventArgs e)
        {
            if (!IsTimeValid())
            {
                await DisplayAlert("CHYBA", "Vami zadaný čas nie je správny.", "OK");
                return;
            }
            
            // kód PDF
        }

        private bool IsTimeValid()
        {
            return ArriveTime.Time < EndTime.Time
                && ArriveTime.Time < RestStartTime.Time
                && ArriveTime.Time < RestEndTime.Time;
        }
    }
}
