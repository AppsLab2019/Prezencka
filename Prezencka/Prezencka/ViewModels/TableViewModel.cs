using Prezencka.Models;
using Prezencka.Services;
using Prezencka.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        private readonly WorkDayStore _workDayStore;

        public TableViewModel()
        {
            _workDayStore = App.WorkDayStore;

            RefreshCommand = new Command(Refresh);
            PreviousMonthCommand = new Command(HandlePreviousMonth);
            NextMonthCommand = new Command(HandleNextMonth);
            EditCommand = new Command<WorkDay>(HandleEdit);
            DeleteCommand = new Command<WorkDay>(HandleDelete);
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
            {
                var reversedInfo = retrievedInfo.OrderByDescending(day => day.Date);
                WorkDays = new ObservableCollection<WorkDay>(reversedInfo);
            }

            RaisePropertyChanged(nameof(WorkDays));
        }

        private async void HandleEdit(WorkDay day)
        {
            if (day is null)
                return;

            var view = new EditDay { BindingContext = new EditDayViewModel(day) };
            await Shell.Current.Navigation.PushAsync(view);
        }

        private async void HandleDelete(WorkDay day)
        {
            if (day is null)
                return;

            var shouldDelete = await DisplayAlert("Confirmation",
                $"Are you sure you want to delete an entry for {day.Date:dd.MM.yyyy}?", "Yes", "No");

            if (!shouldDelete)
                return;

            await _workDayStore.RemoveAsync(day);
            WorkDays.Remove(day);
        }
    }
}
