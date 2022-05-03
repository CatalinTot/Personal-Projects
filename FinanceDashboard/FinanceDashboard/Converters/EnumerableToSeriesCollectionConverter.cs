using FinanceDashboard.ViewModel.Models;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace FinanceDashboard.Converters
{
    public class EnumerableToSeriesCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            VerifyNullParam(parameter);
            VerifyNullParam(value);

            if (parameter.ToString() == "PieSeries")
            {
                var series = new SeriesCollection();
                foreach (var item in (IEnumerable<CustomPieSeries>)value)
                {
                    series.Add(new PieSeries { Title = item.Title, Values = new ChartValues<ObservableValue>(item.Values), DataLabels = item.DataLabels });
                }

                return series;
            }

            return new SeriesCollection { new LineSeries { Values = new ChartValues<ObservableValue>((IEnumerable<ObservableValue>)value) } };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private void VerifyNullParam(object param)
        {
            if (param != null)
            {
                return;
            }

            throw new ArgumentNullException(nameof(param));
        }
    }
}
