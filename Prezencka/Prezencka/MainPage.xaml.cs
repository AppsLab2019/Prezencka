using System;
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
                clock_big.Text = DateTime.Now.ToString("HH:mm:ss");
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
            else
            {
                await DisplayAlert("VLASTNÝ ČAS ZAZNAMENANÝ", "Váš čas bol úspešne zaznamenaný do pracovného výkazu.", "OK");
            }
            
            // kód PDF
        }

        private bool IsTimeValid()
        {
            return ArriveTime.Time <= LeaveTime.Time
                && ArriveTime.Time < RestStartTime.Time
                && ArriveTime.Time < RestEndTime.Time;
        }
    }
}
