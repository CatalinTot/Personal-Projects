using Chess.EventAggregatorHandler;
using Chess.EventAggregatorMessages;
using Chess.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Network
{
    public class Receiver : IReceiver
    {
        private readonly IInterfaceActualizer actualizer;
        private readonly IMoveExecutor executor;
        private readonly IEventAggregator eventAggregator;
        private readonly IReceiverWrapper socketWrapper;

        public Receiver(IInterfaceActualizer actualizer, IMoveExecutor executor)
        {
            this.actualizer = actualizer;
            this.executor = executor;
            eventAggregator = EventAggregatorSingleton.Instance;
            socketWrapper = new SocketWrapper();
        }

        public async void ConnectTo(string ip)
        {
            try
            {
                eventAggregator.SendMessage(new UpdatesMessage("Connectig to the server...", Visibility.Visible, false));
                await socketWrapper.ConnectAsync(ip);
                await ReceiveCommandAsync();
                eventAggregator.SendMessage(new UpdatesMessage("Connected!", Visibility.Hidden, true));
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode == SocketError.ConnectionReset)
                {
                    eventAggregator.SendMessage(new UpdatesMessage("Connection Refused!", Visibility.Hidden, true));
                    eventAggregator.SendMessage(new DisableConnectClientButtonMessage(false));
                    return;
                }

                if (e.SocketErrorCode == SocketError.OperationAborted)
                {
                    eventAggregator.SendMessage(new UpdatesMessage("The server is stopped!", Visibility.Hidden, true));
                    eventAggregator.SendMessage(new DisableConnectClientButtonMessage(false));
                    return;
                }

                throw e;
            }
        }

        public void Stop()
        {
            socketWrapper.Disconnect();
        }

        private void ExecuteRemoteCommand(string message)
        {
            while (message.IndexOf('_') > -1)
            {
                var data = message[..message.IndexOf('_')].Split(',', StringSplitOptions.RemoveEmptyEntries);
                executor.ExecuteMove(data[0], int.Parse(data[1]), bool.Parse(data[^1]), out IEnumerable<IPiece> pieces);
                actualizer.UpdateInterface(pieces);
                eventAggregator.SendMessage(new UpdatesMessage("Command executed! Ready for other commands!", Visibility.Hidden, true));
                message = message[(message.IndexOf('_') + 1) ..];
            }
        }

        private async Task ReceiveCommandAsync()
        {
            try
            {
                while (true)
                {
                    var message = await socketWrapper.ReceiveCommandAsync();
                    ExecuteRemoteCommand(message);
                }
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode == SocketError.OperationAborted)
                {
                    eventAggregator.SendMessage(new UpdatesMessage("You are disconnected!", Visibility.Hidden, true));
                    return;
                }

                throw e;
            }
            catch (ObjectDisposedException)
            {
                eventAggregator.SendMessage(new UpdatesMessage("You are disconnected!", Visibility.Hidden, true));
            }
        }
    }
}
