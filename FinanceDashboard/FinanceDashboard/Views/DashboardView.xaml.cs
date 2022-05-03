using FinanceDashboard.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace FinanceDashboard.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        protected DashboardViewModel Model
        {
            get { return (DashboardViewModel)Resources["DashboardViewModel"]; }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var window = new AccountsListView();
            window.Show();
        }
    }
}
