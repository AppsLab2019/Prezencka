using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prezencka.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected Task DisplayAlert(string title, string message, string cancel) =>
            Application.Current.MainPage.DisplayAlert(title, message, cancel);

        protected Task<bool> DisplayAlert(string title, string message, string confirm, string cancel) =>
            Application.Current.MainPage.DisplayAlert(title, message, confirm, cancel);

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(value, backingField))
                return;

            backingField = value;
            RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void RaiseAllPropertiesChanged() =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }
}
