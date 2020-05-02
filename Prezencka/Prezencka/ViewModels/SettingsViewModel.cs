using Prezencka.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prezencka.ViewModels
{
    public sealed class SettingsViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Company { get; set; }
        public TimeSpan WorkingTime { get; set; }
        public TimeSpan RestTime { get; set; }
        public TimeSpan ArriveTime { get; set; }
        public TimeSpan RestStartTime { get; set; }
        public TimeSpan LeaveTime { get; set; }

        public ICommand LoadCommand { get; }
        public ICommand SaveCommand { get; }

        private readonly SettingsService _settingsSerivce;

        public SettingsViewModel()
        {
            _settingsSerivce = App.SettingsService;

            LoadCommand = new Command(LoadSettings);
            SaveCommand = new Command(async () => await SaveSettings());
        }

        private void LoadSettings()
        {
            Name = _settingsSerivce.Name;
            Id = _settingsSerivce.Id;
            Company = _settingsSerivce.Company;
            WorkingTime = _settingsSerivce.WorkingTime;
            RestTime = _settingsSerivce.RestTime;
            ArriveTime = _settingsSerivce.ArriveTime;
            RestStartTime = _settingsSerivce.RestStartTime;
            LeaveTime = _settingsSerivce.LeaveTime;

            RaiseAllPropertiesChanged();
        }

        private async Task SaveSettings()
        {
            if (!IsTimeValid())
            {
                await DisplayAlert("CHYBA", "Vami zadaný čas nie je správny.", "OK");
                return;
            }

            _settingsSerivce.Name = Name;
            _settingsSerivce.Id = Id;
            _settingsSerivce.Company = Company;
            _settingsSerivce.WorkingTime = WorkingTime;
            _settingsSerivce.RestTime = RestTime;
            _settingsSerivce.ArriveTime = ArriveTime;
            _settingsSerivce.RestStartTime = RestStartTime;
            _settingsSerivce.LeaveTime = LeaveTime;

            await _settingsSerivce.FlushSettings();
            await DisplayAlert("ULOŽENÉ", "Vaše nastavenia boli úspeśne uložené.", "OK");
        }

        private static bool IsTimeValid()
        {
            return true;
        }
    }
}
