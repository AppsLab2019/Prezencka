using Prezencka.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace Prezencka.Converters
{
    public sealed class YearMonthToFormattedStringConverter : IValueConverter
    {
        private readonly IDictionary<int, string> _numberToString;

        public YearMonthToFormattedStringConverter()
        {
            _numberToString = new Dictionary<int, string>
            {
                [0] = "Month",
                [1] = "Január",
                [2] = "Február",
                [3] = "Marec",
                [4] = "Apríl",
                [5] = "Máj",
                [6] = "Jún",
                [7] = "Júl",
                [8] = "August",
                [9] = "September",
                [10] = "Október",
                [11] = "November",
                [12] = "December"
            };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is YearMonth yearMonth))
                return null;

            return $"{_numberToString[yearMonth.Month]} {yearMonth.Year}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
