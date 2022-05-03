using System.Windows.Controls;
using Chess.ViewModel;

namespace Chess.Views
{
    /// <summary>
    /// Interaction logic for PiecesView.xaml
    /// </summary>
    public partial class PlayerPiecesView : UserControl
    {
        public PlayerPiecesView()
        {
            InitializeComponent();
        }

        protected PlayerPiecesViewModel Model
        {
            get { return (PlayerPiecesViewModel)Resources["PlayerPiecesViewModel"]; }
        }
    }
}
