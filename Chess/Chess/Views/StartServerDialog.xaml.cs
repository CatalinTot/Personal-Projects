using Chess.ViewModel;
using System.Windows;

namespace Chess.Views
{
    public partial class StartServerDialog : Window
    {
        public StartServerDialog(bool isConnected)
        {
            InitializeComponent();
            Model.IsConnected = isConnected;
        }

        protected ServerDialogViewModel Model
        {
            get { return (ServerDialogViewModel)Resources["ServerDialogViewModel"]; }
        }
    }
}
