using Chess.EventAggregatorHandler;
using Chess.EventAggregatorMessages;
using System.Windows;
using System.Windows.Input;

namespace Chess.ViewModel
{
    public class ClientDialogViewModel : ViewModelBase
    {
        private readonly IEventAggregator eventAggregator;
        private bool isConnected;
        private string defaultIpAddress = "127.0.0.1";
        private ICommand connectCommand;
        private ICommand closeCommand;
        private ICommand disconnectCommand;

        public ClientDialogViewModel() : this(EventAggregatorSingleton.Instance)
        {
        }

        public ClientDialogViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public string DefaultIpAddress
        {
            get => defaultIpAddress;
            set
            {
                defaultIpAddress = value;
                OnPropertyChanged(nameof(DefaultIpAddress));
            }
        }

        public bool IsConnected
        {
            get => isConnected;
            set
            {
                isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }

        public ICommand ConnectCommand
        {
            get
            {
                connectCommand ??= new RelayCommand(param => Connect(param));
                return connectCommand;
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                closeCommand ??= new RelayCommand(param => CloseDialog(param));
                return closeCommand;
            }
        }

        public ICommand DisconnectCommand
        {
            get
            {
                disconnectCommand ??= new RelayCommand(param => Disconnect(param));
                return disconnectCommand;
            }
        }

        private void Disconnect(object param)
        {
            eventAggregator.SendMessage(new DisconnectClientMessage());
            eventAggregator.SendMessage(new DisableConnectClientButtonMessage(false));
            CloseDialog(param);
        }

        private void CloseDialog(object obj)
        {
            if (obj is not Window window)
            {
                return;
            }

            window.Close();
        }

        private void Connect(object obj)
        {
            eventAggregator.SendMessage(new ConnectClientMessage(DefaultIpAddress));
            eventAggregator.SendMessage(new HaltCommandMessage());
            eventAggregator.SendMessage(new DisableConnectClientButtonMessage(true));
            CloseDialog(obj);
        }
    }
}
