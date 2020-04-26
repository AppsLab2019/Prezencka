using Xamarin.Forms;
using System;

namespace Prezencka.Views
{
    public partial class Holidays : ContentPage
    {
        public Holidays() => 
            InitializeComponent();

        private async void OnConfirm(object sender, System.EventArgs e)
        {
            if (!IsDateValid(sender, e))
            {
                await DisplayAlert("CHYBA", "Vami zadaný dátum nie je správny.", "OK");
            }
            else
            {
                var result = await DisplayActionSheet("ABSENCIA", "OK", null, "Dovolenka", "Lekár", "PN", "OČR");

                if (result is null || result == "")
                    return;
                
                await DisplayAlert("ABSENCIA ZAZNAMENANÁ", "Vaša absencia bola úspešne zaznamenaná do pracovného výkazu.", "OK");
            }

          
        }       
        private async void OnCancel(object sender, EventArgs e)
        {
            await DisplayAlert("ABSENCIA ZRUŠENÁ", "Vaša absencia bola úspešne zrušená.", "OK");
        }
        private bool IsDateValid(object sender, EventArgs e)
        {
            return date_from_picker.Date <= date_to_picker.Date;
        }

        private void DateFromPicker(object sender, DateChangedEventArgs e)
        {
            //pdf
        }

        private void DateToPicker(object sender, DateChangedEventArgs e)
        {
            //pdf
        }


    }
} 
