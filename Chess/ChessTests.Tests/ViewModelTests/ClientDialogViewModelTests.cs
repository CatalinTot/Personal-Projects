using Chess.EventAggregatorHandler;
using Chess.EventAggregatorMessages;
using Chess.ViewModel;
using Xunit;

namespace ChessTests.Tests.ViewModelTests
{
    public class ClientDialogViewModelTests
    {
        [Fact]
        public void ClientDialogViewModelTests_CheckTheDefaultIpAddress_ShouldReturnTrue()
        {
            var viewModel = new ClientDialogViewModel();

            Assert.Equal("127.0.0.1", viewModel.DefaultIpAddress);
        }

        [Fact]
        public void ClientDialogViewModelTests_CheckIfTheIpAddressHasChanged_ShouldReturnTrue()
        {
            var viewModel = new ClientDialogViewModel();
            viewModel.DefaultIpAddress = "192.168.1.3";

            Assert.Equal("192.168.1.3", viewModel.DefaultIpAddress);
        }

        [Fact]
        public void ClientDialogViewModelTests_CheckTheIsConnectedProperty_ShouldReturnFalse()
        {
            var viewModel = new ClientDialogViewModel();

            Assert.False(viewModel.IsConnected);
        }

        [Fact]
        public void ClientDialogViewModelTests_CheckIfTheIsConnectedPropertyHasChanged_ShouldReturnFalse()
        {
            var viewModel = new ClientDialogViewModel();
            viewModel.IsConnected = true;

            Assert.True(viewModel.IsConnected);
        }

        [Fact]
        public void ClientDialogViewModelTests_CheckIfTheClientHasConnectedUponTriggeringTheCommand_ShouldReturnFalse()
        {
            var eventAggregator = new EventAggregator();
            var viewModel = new ClientDialogViewModel(eventAggregator);

            string connectionIpAddress = string.Empty;
            bool isHaltCommand = false;
            bool isEnabledConnectClientButton = false;

            eventAggregator.RegisterHandler<ConnectClientMessage>(message => connectionIpAddress = message.Ip);
            eventAggregator.RegisterHandler<HaltCommandMessage>(message => isHaltCommand = true);
            eventAggregator.RegisterHandler<DisableConnectClientButtonMessage>(message => isEnabledConnectClientButton = message.IsConnected);

            viewModel.ConnectCommand.Execute(null);

            Assert.True(isHaltCommand);
            Assert.True(isEnabledConnectClientButton);
            Assert.Equal("127.0.0.1", connectionIpAddress);
            
        }

        [Fact]
        public void ClientDialogViewModelTests_CheckIfTheClientHasDisconnectedUponTriggeringTheCommand_ShouldReturnFalse()
        {
            var eventAggregator = new EventAggregator();
            var viewModel = new ClientDialogViewModel(eventAggregator);

            bool isConnected = false;
            bool isDisabledConnectClientButton = false;

            eventAggregator.RegisterHandler<DisconnectClientMessage>(message => isDisabledConnectClientButton = true);
            eventAggregator.RegisterHandler<DisableConnectClientButtonMessage>(message => isConnected = message.IsConnected);

            viewModel.DisconnectCommand.Execute(null);

            Assert.False(isConnected);
            Assert.True(isDisabledConnectClientButton);
            
        }
    }
}
