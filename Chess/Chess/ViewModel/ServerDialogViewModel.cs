using Chess.EventAggregatorHandler;
using Chess.EventAggregatorMessages;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;

namespace Chess.ViewModel
{
    public class ServerDialogViewModel : ViewModelBase
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IPAddress defaultIpAddress;
        private bool isConnected;
        private ICommand startServerCommand;
        private ICommand stopServerCommand;

        public ServerDialogViewModel()
        {
            eventAggregator = EventAggregatorSingleton.Instance;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    defaultIpAddress = ip;
                    break;
                }
            }
        }

        public string DefaultIpAddress
        {
            get
            {
                return $"Current IP address: {defaultIpAddress}";
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

        public ICommand StartServerCommand
        {
            get
            {
                startServerCommand ??= new RelayCommand(param => StartServer(param));
                return startServerCommand;
            }
        }

        public ICommand StopServerCommand
        {
            get
            {
                stopServerCommand ??= new RelayCommand(param => StopServer(param));
                return stopServerCommand;
            }
        }

        private void StopServer(object param)
        {
            eventAggregator.SendMessage(new StopServerMessage());
            eventAggregator.SendMessage(new DisableConnectServerButtonMessage(false));
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

        private void StartServer(object param)
        {
            eventAggregator.SendMessage(new ConnectServerMessage($"{defaultIpAddress}"));
            eventAggregator.SendMessage(new DisableConnectServerButtonMessage(true));
            CloseDialog(param);
        }
    }
}
