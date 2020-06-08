using Prezencka.Models;
using Prezencka.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prezencka.ViewModels
{
    public sealed class MainViewModel : BaseViewModel
    {
        public DateTime CurrentTime
        {
            get => _currentTime;
            set => OnPropertyChanged(ref _currentTime, value);
        }
        private DateTime _currentTime;

        public string Reminder
        {
            get => _reminder;
            set => OnPropertyChanged(ref _reminder, value);
        }
        private string _reminder;

        public ICommand InitCommand { get; }
        public ICommand ArriveCommand { get; }
        public ICommand RestCommand { get; }
        public ICommand LeaveCommand { get; }

        private WorkDay _day;
        private readonly WorkDayStore _store;
        private readonly SettingsService _settings;

        public MainViewModel()
        {
            _store = App.WorkDayStore;
            _settings = App.SettingsService;

            InitCommand = new Command(Initialize);
            ArriveCommand = new Command(async () => await HandleArrive());
            RestCommand = new Command(async () => await HandleRest());
            LeaveCommand = new Command(async () => await HandleLeave());

            Initialize();

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                RefreshTime();
                RefreshReminder();
                return true;
            });

            RefreshTime();
            RefreshReminder();
        }

        public void Initialize()
        {
            var today = DateTime.Today;
            var retrievedDay = _store.GetWorkDay(today);

            if (!(retrievedDay is null))
            {
                _day = retrievedDay;
                return;
            }

            _day = new WorkDay
            {
                Date = today
            };
        }

        private void RefreshTime()
        {
            CurrentTime = DateTime.Now;
        }

        private void RefreshReminder()
        {
            if (_day.ArriveTime == default)
                Reminder = FormatReminder("príchodu", _settings.ArriveTime);
            else if (_day.RestStartTime == default)
                Reminder = FormatReminder("začiatku prestávky", _settings.RestStartTime);
            else if (_day.RestStopTime == default)
                Reminder = FormatReminder("konca prestávky", _day.RestStartTime + _settings.RestTime);
            else if (_day.LeaveTime == default)
                Reminder = FormatReminder("odchodu", _settings.LeaveTime);
            else
                Reminder = $"Užite si voľno!";
        }

        private string FormatReminder(string what, TimeSpan recurringTime)
        {
            var currentTime = DateTime.Now.TimeOfDay;
            var time = recurringTime - currentTime;
            var showMinus = currentTime.Ticks > recurringTime.Ticks;

            return $"Do pravidelného {what} Vám zostáva {(showMinus ? "-" : "")}{time:hh\\:mm\\:ss}!";
        }

        public async Task HandleArrive()
        {
            if (!await ConfirmIfAlreadySet(_day.ArriveTime))
                return;

            _day.ArriveTime = DateTime.Now.TimeOfDay;
            await _store.AddOrUpdateAsync(_day);
            await DisplayAlert("PRÍCHOD ZAZNAMENANÝ", "Váš príchod bol úspešne zaznamenaný do pracovného výkazu.", "OK");
        }

        public async Task HandleRest()
        {
            var currentTime = DateTime.Now.TimeOfDay;

            if (_day.RestStopTime != default)
                return;

            if (_day.RestStartTime == default)
                _day.RestStartTime = currentTime;
            else
                _day.RestStopTime = currentTime;

            await _store.AddOrUpdateAsync(_day);
            await DisplayAlert("PRESTÁVKA ZAZNAMENANÁ", "Vaša prestávka bola úspešne zaznamenaná do pracovného výkazu.", "OK");
        }

        public async Task HandleLeave()
        {
            if (!await ConfirmIfAlreadySet(_day.LeaveTime))
                return;

            _day.LeaveTime = DateTime.Now.TimeOfDay;
            await _store.AddOrUpdateAsync(_day);
            await DisplayAlert("ODCHOD ZAZNAMENANÝ", "Váš odchod bol úspešne zaznamenaný do pracovného výkazu.", "OK");
        }

        private Task<bool> ConfirmIfAlreadySet(TimeSpan time)
        {
            if (time == default)
                return Task.FromResult(true);

            return DisplayAlert("POTVRDENIE", $"Ste si istý že chcete prepísať nasledujúci čas? ({time:hh\\:mm})", "Áno", "Nie");
        }
    }
}
