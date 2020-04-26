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

        public ICommand InitCommand { get; }
        public ICommand ArriveCommand { get; }
        public ICommand RestCommand { get; }
        public ICommand LeaveCommand { get; }

        private WorkDay _day;
        private readonly WorkDayStore _store;

        public MainViewModel()
        {
            _store = App.WorkDayStore;

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                CurrentTime = DateTime.Now;
                return true;
            });

            InitCommand = new Command(Initialize);
            ArriveCommand = new Command(async () => await HandleArrive());
            RestCommand = new Command(async () => await HandleRest());
            LeaveCommand = new Command(async () => await HandleLeave());
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

        public async Task HandleArrive()
        {
            _day.ArriveTime = DateTime.Now.TimeOfDay;
            await _store.AddOrUpdateAsync(_day);
            await DisplayAlert("PRÍCHOD ZAZNAMENANÝ", "Váš príchod bol úspešne zaznamenaný do pracovného výkazu.", "OK");
        }

        public async Task HandleRest()
        {
            var currentTime = DateTime.Now.TimeOfDay;

            if (_day.RestStartTime == default)
                _day.RestStartTime = currentTime;
            else
                _day.RestStopTime = currentTime;

            await _store.AddOrUpdateAsync(_day);
            await DisplayAlert("PRESTÁVKA ZAZNAMENANÁ", "Vaša prestávka bola úspešne zaznamenaná do pracovného výkazu.", "OK");
        }

        public async Task HandleLeave()
        {
            _day.LeaveTime = DateTime.Now.TimeOfDay;
            await _store.AddOrUpdateAsync(_day);
            await DisplayAlert("ODCHOD ZAZNAMENANÝ", "Váš odchod bol úspešne zaznamenaný do pracovného výkazu.", "OK");
        }
    }
}
