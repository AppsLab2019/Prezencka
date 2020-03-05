using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using System;

namespace Prezencka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Holidays : ContentPage
    {
        public Holidays() => 
            InitializeComponent();

        private async void OnConfirm(object sender, System.EventArgs e)
        {
            if (!IsDateValid(sender, e))
            {
                await DisplayAlert("CHYBA", "Vami zadaný čas nie je správny.", "OK");
                return;
            }

            // kód PDF
        }
        private bool IsDateValid(object sender, EventArgs e)
        {
            return date_to_picker.Date < date_from_picker.Date;
        }
    }
} 
