using System;
using System.Windows.Controls;
using Chess.ViewModel;

namespace Chess.Views
{
    public partial class RelocationsGridView : UserControl
    {
        public RelocationsGridView()
        {
            InitializeComponent();
            Model.PropertyChanged += ModelScrollOffsetPropertyChanged;
        }

        public RelocationsGridViewModel Model
        {
            get { return (RelocationsGridViewModel)Resources["RelocationsGridViewModel"]; }
        }

        private void ModelScrollOffsetPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Scroller.ScrollToVerticalOffset(Model.RelocationsScrollOffset);
        }
    }
}
