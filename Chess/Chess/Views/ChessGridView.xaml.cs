using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Chess.ViewModel;

namespace Chess.Views
{
    public partial class ChessGridView : UserControl
    {
        public ChessGridView()
        {
            InitializeComponent();
        }

        protected ChessGridViewModel Model
        {
            get { return (ChessGridViewModel)Resources["ChessGridViewModel"]; }
        }
    }
}
