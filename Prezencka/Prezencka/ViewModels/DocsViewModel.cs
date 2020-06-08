using Prezencka.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prezencka.ViewModels
{
    public sealed class DocsViewModel : BaseViewModel
    {
        public ICommand TicketCommand { get; }
        public ICommand HolidayCommand { get; }
        public ICommand TimeSheetCommand { get; }

        private readonly string _extractPath;

        public DocsViewModel()
        {
            TicketCommand = new Command(async () => await OpenTicket());
            HolidayCommand = new Command(async () => await OpenHoliday());
            TimeSheetCommand = new Command(async () => await OpenTimeSheet());
            
            _extractPath = "/storage/emulated/0/";
        }

        private Task OpenTicket()
        {
            return ExtractDocument("priepustka.pdf");
        }
        
        private Task OpenHoliday()
        {
            return DisplayAlert("CHYBA!", "Tento súbor nie je momentálne dostupný!", "Ok");
        }
        
        private Task OpenTimeSheet()
        {
            return ExtractDocument("pracovny_vykaz.pdf");
        }

        private async Task ExtractDocument(string name)
        {
            var path = System.IO.Path.Combine(_extractPath, name);

            bool result;
            try
            {
                result = await FileExtractor.Extract($"Prezencka.{name}", path);
            }
            catch (Exception)
            {
                await DisplayAlert("KRITICKÁ CHYBA!", "Súbor nebol uložený kvôli neznámej chybe!", "Ok");
                return;
            }

            if (result)
                await DisplayAlert("ÚSPECH!", $"Súbor {name} bol úspešne uložený!", "Ok");
            else
                await DisplayAlert("CHYBA!", $"Nepodarilo sa uložiť súbor {name}. Sú potrebné práva k súborom.", "Ok");
        }
    }
}
