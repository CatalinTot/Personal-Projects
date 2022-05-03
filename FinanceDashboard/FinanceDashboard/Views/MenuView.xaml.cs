using FinanceDashboard.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace FinanceDashboard.Views
{
    public partial class MenuView : UserControl
    {
        public static readonly DependencyProperty CurrentViewModelProperty =
        DependencyProperty.Register(
            "CurrentViewModel",
            typeof(object),
            typeof(MenuView));

        public MenuView()
        {
            InitializeComponent();
            CurrentViewModel = Model.CurrentViewModel;
            Model.PropertyChanged += ModelPropertyChanged;
        }

        public object CurrentViewModel
        {
            get { return GetValue(CurrentViewModelProperty); }
            set { SetValue(CurrentViewModelProperty, value); }
        }

        protected MenuViewModel Model
        {
            get { return (MenuViewModel)Resources["MenuViewModel"]; }
        }

        private void ModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CurrentViewModel = Model.CurrentViewModel;
        }
    }
}
