using FinanceDashboard.ViewModel.Models;
using LiveCharts.Defaults;
using System.Collections.Generic;
using Xunit;

namespace FinanceDashboard.Tests
{
    public class CustomPieSeriesTests
    {   
        [Fact]
        public void CheckIfTheItemsWereAddedIntoTheCollection()
        {
            var pieSeries = new[] { new CustomPieSeries() { DataLabels = true, Title = "Title" } };
            var series = new List<CustomPieSeries>(pieSeries);
        }
    }
}