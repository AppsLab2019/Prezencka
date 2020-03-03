using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace Prezencka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Holidays : ContentPage
    {
        public Holidays() => 
            InitializeComponent();

        private void DateFromPicker(object sender, DateChangedEventArgs e)
        {
            Recalculate();
        }

        private void DateToPicker(object sender, DateChangedEventArgs e)
        {
            Recalculate();
        }

        private async void Recalculate()
        {
            if (date_to_picker.Date < date_from_picker.Date)
            {
                bool answer = await DisplayAlert("CHYBA", "Vami zadaný dátum nie je správny.", "", "OK");
                Debug.WriteLine("Answer: " + answer);
            }
            else
            {
                return;
            }
        }
    }
} 
