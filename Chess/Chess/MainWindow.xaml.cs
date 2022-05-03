using System.Windows;
using Chess.ViewModel;

namespace Chess
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected MainWindowViewModel Model
        {
            get { return (MainWindowViewModel)Resources["MainWindowViewModel"]; }
        }
    }
}
