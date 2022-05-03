using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.Generic;

namespace FinanceDashboard.ViewModel.Models
{
    public class CustomPieSeries
    {
        public CustomPieSeries()
        {
            Values = new List<ObservableValue>();
        }

        public IEnumerable<ObservableValue> Values { get; set; }

        public string Title { get; set; }

        public bool DataLabels { get; set; }
    }
}
