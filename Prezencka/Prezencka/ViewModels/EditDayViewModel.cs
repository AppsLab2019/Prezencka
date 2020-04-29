using Prezencka.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prezencka.ViewModels
{
    public sealed class EditDayViewModel : BaseViewModel
    {
        public DateTime Date { get; set; }
        public TimeSpan ArriveTime { get; set; }
        public TimeSpan RestStartTime { get; set; }
        public TimeSpan RestStopTime { get; set; }
        public TimeSpan LeaveTime { get; set; }
        public string Comment { get; set; }

        public ICommand BackCommand { get; }
        public ICommand SaveCommand { get; }

        private readonly WorkDay _day;

        public EditDayViewModel(WorkDay day)
        {
            _day = day;
            Refresh();

            BackCommand = new Command(async () => await Back());
            SaveCommand = new Command(async () => await Save());
        }

        private void Refresh()
        {
            Date = _day.Date;
            ArriveTime = _day.ArriveTime;
            RestStartTime = _day.RestStartTime;
            RestStopTime = _day.RestStopTime;
            LeaveTime = _day.LeaveTime;
            Comment = _day.Comment;
        }

        private bool HasChanged()
        {
            var isSame = Date == _day.Date
                && ArriveTime == _day.ArriveTime
                && RestStartTime == _day.RestStartTime
                && RestStopTime == _day.RestStopTime
                && LeaveTime == _day.LeaveTime
                && Comment == _day.Comment;

            return !isSame;
        }

        private async Task Back()
        {
            if (HasChanged())
            {
                var discard = await DisplayAlert("Confirmation", "Are you sure you want to discard changes?", "Yes", "No");

                if (!discard)
                    return;
            }

            await Shell.Current.Navigation.PopAsync();
        }

        private async Task Save()
        {
            _day.Date = Date;
            _day.ArriveTime = ArriveTime;
            _day.RestStartTime = RestStartTime;
            _day.RestStopTime = RestStopTime;
            _day.LeaveTime = LeaveTime;
            _day.Comment = Comment;

            await App.WorkDayStore.UpdateAsync(_day);
            await DisplayAlert("Success", "Information saved successfully!", "Ok");
        }
    }
}
