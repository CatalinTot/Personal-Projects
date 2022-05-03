using Chess.ViewModel;
using System.Windows;

namespace Chess.Views
{
    public partial class ConnectClientDialog : Window
    {
        public ConnectClientDialog(bool isConnected)
        {
            InitializeComponent();
            Model.IsConnected = isConnected;
        }

        protected ClientDialogViewModel Model
        {
            get { return (ClientDialogViewModel)Resources["ClientDialogViewModel"]; }
        }
    }
}
