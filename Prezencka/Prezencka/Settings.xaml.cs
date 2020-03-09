using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prezencka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        private bool IsTimeValid(object sender, EventArgs e)
        {
            return leaveTime.Time - arriveTime.Time == workingTime.Time;
        }

        private async void Save(object sender, EventArgs e)
        {
            if (!IsTimeValid(sender, e))
            {
                await DisplayAlert("CHYBA", "Vami zadaný čas nie je správny.", "OK");
                return;
            }
            else
            {
                await DisplayAlert("ULOŽENÉ", "Vaše nastavenia boli úspeśne uložené.", "OK");
            }
        }
    }
}