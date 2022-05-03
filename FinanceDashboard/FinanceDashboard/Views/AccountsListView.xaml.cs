using FinanceDashboard.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FinanceDashboard.Views
{
    public partial class AccountsListView : Window
    {
        public AccountsListView()
        {
            InitializeComponent();
        }

        protected AccountsListViewModel Model
        {
            get { return (AccountsListViewModel)Resources["AccountsListViewModel"]; }
        }

        private void BorderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Model.SelectAccountCommand.Execute(((Border)sender).Tag);
            Close();
        }
    }
}
