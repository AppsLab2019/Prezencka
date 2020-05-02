using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prezencka.Services
{
    public sealed class SettingsService
    {
        public string Name 
        { 
            get => Get<string>("Name");
            set => _properties["Name"] = value;
        }

        public string Id
        {
            get => Get<string>("Id");
            set => _properties["Id"] = value;
        }

        public string Company
        {
            get => Get<string>("Company");
            set => _properties["Company"] = value;
        }

        public TimeSpan WorkingTime
        {
            get => Get("WorkingTime", new TimeSpan(8, 0, 0));
            set => _properties["WorkingTime"] = value;
        }
        public TimeSpan RestTime
        {
            get => Get("RestTime", new TimeSpan(0, 30, 0));
            set => _properties["RestTime"] = value;
        }

        public TimeSpan ArriveTime
        {
            get => Get("ArriveTime", new TimeSpan(8, 0, 0));
            set => _properties["ArriveTime"] = value;
        }

        public TimeSpan RestStartTime
        {
            get => Get("RestStartTime", new TimeSpan(12, 0, 0));
            set => _properties["RestStartTime"] = value;
        }

        public TimeSpan LeaveTime
        {
            get => Get("LeaveTime", new TimeSpan(16, 0, 0));
            set => _properties["LeaveTime"] = value;
        }

        private readonly IDictionary<string, object> _properties;

        public SettingsService()
        {
            _properties = Application.Current.Properties;
        }

        public Task FlushSettings()
        {
            return Application.Current.SavePropertiesAsync();
        }

        private T Get<T>(string key, T @default = default)
        {
            if (!_properties.ContainsKey(key))
                return @default;

            var raw = _properties[key];

            if (raw is null)
                return default;

            if (raw is T casted)
                return casted;

            throw new Exception($"'{key}' is type {raw.GetType().Name}, not {typeof(T).Name}");
        }
    }
}
