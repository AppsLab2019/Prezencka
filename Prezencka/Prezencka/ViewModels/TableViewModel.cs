using Prezencka.Models;
using Prezencka.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prezencka.ViewModels
{
    public sealed class TableViewModel : INotifyPropertyChanged
    {
        public YearMonth YearMonth { get; private set; }
        public ObservableCollection<WorkDay> WorkDays { get; private set; }

        public ICommand PreviousMonthCommand { get; }
        public ICommand NextMonthCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly WorkDayStore _workDayStore;

        public TableViewModel()
        {
            PreviousMonthCommand = new Command(HandlePreviousMonth);
            NextMonthCommand = new Command(HandleNextMonth);

            var date = DateTime.Today;
            YearMonth = new YearMonth(date.Year, date.Month);

            _workDayStore = App.WorkDayStore;

            UpdateWorkDays();
        }

        private void HandlePreviousMonth()
        {
            var year = YearMonth.Year;
            var month = YearMonth.Month;

            --month;

            if (month <= 0)
            {
                --year;
                month = 12;
            }

            YearMonth = new YearMonth(year, month);

            RaisePropertyChanged(nameof(YearMonth));
            UpdateWorkDays();
        }

        private void HandleNextMonth()
        {
            var year = YearMonth.Year;
            var month = YearMonth.Month;

            ++month;

            if (month >= 13)
            {
                ++year;
                month = 1;
            }

            YearMonth = new YearMonth(year, month);

            RaisePropertyChanged(nameof(YearMonth));
            UpdateWorkDays();
        }

        private void UpdateWorkDays()
        {
            WorkDays = new ObservableCollection<WorkDay>(_workDayStore.GetWorkDays(YearMonth));
            RaisePropertyChanged(nameof(WorkDays));
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
