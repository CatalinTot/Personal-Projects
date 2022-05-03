using LiveCharts.Defaults;
using Microsoft.Maps.MapControl.WPF;
using System.Collections.Generic;

namespace FinanceDashboard.ViewModel.Models
{
    public class AccountModel
    {
        public string Balance { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Abreviation { get; set; }

        public string Symbol { get; set; }

        public IEnumerable<TransactionModel> Transactions { get; set; }

        public IEnumerable<ObservableValue> Spendings { get; set; }

        public IEnumerable<Location> NearbyRewards { get; set; }

        public IEnumerable<CustomPieSeries> PortofolioItems { get; set; }
    }
}
