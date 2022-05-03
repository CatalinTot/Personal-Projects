using System;
using System.Globalization;
using System.Windows.Data;

namespace FinanceDashboard.Converters
{
    [ValueConversion(typeof(bool), typeof(object))]
    public class NotBoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(bool)value)
            {
                return (string)parameter == "MaxWidth" ? double.PositiveInfinity : "*";
            }

            return (string)parameter == "MaxWidth" ? 0.0 : "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}