using Chess.EventAggregatorHandler;
using FinanceDashboard.EventAggregatorMessages;
using System.Windows;
using System.Windows.Input;

namespace FinanceDashboard.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ICommand closeWindowCommand;
        private Visibility mainWindowShadowVisibility = Visibility.Hidden;

        public MainWindowViewModel()
        {
            IEventAggregator eventAggregator = EventAggregatorSingleton.Instance;
            eventAggregator.RegisterHandler<ChangeWindowOpacityMessage>(OpacityChangedEventHandler);
        }

        public ICommand CloseWindowCommand
        {
            get
            {
                closeWindowCommand ??= new RelayCommand(param => CloseWindow(param));
                return closeWindowCommand;
            }
        }

        public Visibility MainWindowShadowVisibility
        {
            get => mainWindowShadowVisibility;
            set
            {
                mainWindowShadowVisibility = value;
                OnPropertyChanged(nameof(MainWindowShadowVisibility));
            }
        }

        private void CloseWindow(object param)
        {
            if (param is not Window window)
            {
                return;
            }

            window.Close();
        }

        private void OpacityChangedEventHandler(ChangeWindowOpacityMessage message)
        {
            MainWindowShadowVisibility = message.Visibility;
        }
    }
}
