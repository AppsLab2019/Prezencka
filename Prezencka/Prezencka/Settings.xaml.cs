using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Prezencka
{
    public partial class Settings
    {
        private readonly IDictionary<string, object> _properties;

        public Settings()
        {
            InitializeComponent();

            _properties = Application.Current.Properties;

            if (_properties.ContainsKey("Name"))
                NameEntry.Text = (string)_properties["Name"];

            if (_properties.ContainsKey("Id"))
                IdEntry.Text = (string) _properties["Id"];

            if (_properties.ContainsKey("Company"))
                CompanyEntry.Text = (string) _properties["Company"];

            if (_properties.ContainsKey("WorkingTime"))
                WorkingTimePicker.Time = (TimeSpan) _properties["WorkingTime"];

            if (_properties.ContainsKey("RestTime"))
                RestTimePicker.Time = (TimeSpan)_properties["RestTime"];

            if (_properties.ContainsKey("ArriveTime"))
                ArriveTimePicker.Time = (TimeSpan)_properties["ArriveTime"];

            if (_properties.ContainsKey("LeaveTime"))
                LeaveTimePicker.Time = (TimeSpan)_properties["LeaveTime"];
        }

        private bool IsTimeValid()
        {
            return LeaveTimePicker.Time - ArriveTimePicker.Time == WorkingTimePicker.Time;
        }

        private async void Save(object sender, EventArgs e)
        {
            if (!IsTimeValid())
            {
                await DisplayAlert("CHYBA", "Vami zadaný čas nie je správny.", "OK");
                return;
            }

            _properties["Name"] = NameEntry.Text;
            _properties["Id"] = IdEntry.Text;
            _properties["Company"] = CompanyEntry.Text;
            _properties["WorkingTime"] = WorkingTimePicker.Time;
            _properties["RestTime"] = RestTimePicker.Time;
            _properties["ArriveTime"] = ArriveTimePicker.Time;
            _properties["LeaveTime"] = LeaveTimePicker.Time;

            await DisplayAlert("ULOŽENÉ", "Vaše nastavenia boli úspeśne uložené.", "OK");
        }
    }
}