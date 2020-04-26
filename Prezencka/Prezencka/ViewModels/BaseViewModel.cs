using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Prezencka.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected Task DisplayAlert(string title, string message, string cancel) =>
            Application.Current.MainPage.DisplayAlert(title, message, cancel);

        protected Task OpenInBrowser(string uri) =>
            OpenInBrowser(new Uri(uri));

        protected Task OpenInBrowser(Uri uri) =>
            Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);

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
