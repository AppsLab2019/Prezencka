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

        public DocsViewModel()
        {
            TicketCommand = new Command(async () => await OpenTicket());
            HolidayCommand = new Command(async () => await OpenHoliday());
            TimeSheetCommand = new Command(async () => await OpenTimeSheet());
        }

        private Task OpenTicket()
        {
            return OpenInBrowser("https://mojpracovnycas.sk/download/priepustka.pdf");
        }
        
        private Task OpenHoliday()
        {
            return OpenInBrowser("https://mojpracovnycas.sk/download/dovolenka.pdf");
        }
        
        private Task OpenTimeSheet()
        {
            return OpenInBrowser("https://mojpracovnycas.sk/download/pracovny_vykaz.pdf");
        }
    }
}
