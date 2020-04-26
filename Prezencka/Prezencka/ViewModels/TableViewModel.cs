using Prezencka.Models;
using Prezencka.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prezencka.ViewModels
{
    public sealed class TableViewModel : BaseViewModel
    {
        public YearMonth YearMonth { get; private set; }
        public ObservableCollection<WorkDay> WorkDays { get; private set; }

        public ICommand RefreshCommand { get; }
        public ICommand PreviousMonthCommand { get; }
        public ICommand NextMonthCommand { get; }

        private readonly WorkDayStore _workDayStore;

        public TableViewModel()
        {
            _workDayStore = App.WorkDayStore;

            RefreshCommand = new Command(Refresh);
            PreviousMonthCommand = new Command(HandlePreviousMonth);
            NextMonthCommand = new Command(HandleNextMonth);
        }

        private void Refresh()
        {
            var date = DateTime.Today;
            YearMonth = new YearMonth(date.Year, date.Month);

            UpdateWorkDays();
            RaisePropertyChanged(nameof(YearMonth));
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
            var retrievedInfo = _workDayStore.GetWorkDays(YearMonth);

            if (retrievedInfo is null)
                WorkDays = new ObservableCollection<WorkDay>();
            else
                WorkDays = new ObservableCollection<WorkDay>(retrievedInfo);

            RaisePropertyChanged(nameof(WorkDays));
        }
    }
}
