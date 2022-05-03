using System.Windows.Controls;
using Chess.ViewModel;

namespace Chess.Views
{
    public partial class ControlMenuView : UserControl
    {
        public ControlMenuView()
        {
            InitializeComponent();
        }

        protected ControlMenuViewModel Model
        {
            get { return (ControlMenuViewModel)Resources["ControlMenuViewModel"]; }
        }

        private void OpenServerDialog(object sender, System.Windows.RoutedEventArgs e)
        {
            var window = new StartServerDialog(Model.IsDisabledConnectServerButton);
            window.ShowDialog();
        }

        private void OpenClientDialog(object sender, System.Windows.RoutedEventArgs e)
        {
            var window = new ConnectClientDialog(Model.IsDisabledConnectClientButton);
            window.ShowDialog();
        }
    }
}
