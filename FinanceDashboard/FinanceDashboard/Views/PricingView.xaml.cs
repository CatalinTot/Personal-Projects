using FinanceDashboard.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace FinanceDashboard.Views
{
    public partial class PricingView : UserControl
    {
        public PricingView()
        {
            InitializeComponent();
        }

        protected PricingViewModel Model
        {
            get { return (PricingViewModel)Resources["PricingViewModel"]; }
        }
    }
}
