using System.Windows.Input;

namespace FinanceDashboard.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private ICommand menuButtonCommand;
        private object currentViewModel;

        public MenuViewModel()
        {
            currentViewModel = new DashboardViewModel();
        }

        public ICommand ChangeCurrentViewCommand
        {
            get
            {
                return menuButtonCommand ??= new RelayCommand(param => SwitchViews(param));
            }
        }

        public object CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        private void SwitchViews(object param)
        {
            switch (param.ToString())
            {
                case "Dashboard":
                    CurrentViewModel = new DashboardViewModel();
                    break;
                case "Pricing":
                    CurrentViewModel = new PricingViewModel();
                    break;
                default:
                    return;
            }
        }
    }
}
