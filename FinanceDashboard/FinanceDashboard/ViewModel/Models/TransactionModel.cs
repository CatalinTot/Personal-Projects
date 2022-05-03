using System;

namespace FinanceDashboard.ViewModel.Models
{
    public class TransactionModel
    {
        public string Symbol { get; set; }

        public string VendorName { get; set; }

        public DateTime Date { get; set; }

        public string Price { get; set; }
    }
}
