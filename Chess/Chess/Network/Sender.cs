using Chess.EventAggregatorHandler;
using Chess.EventAggregatorMessages;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Network
{
    public class Sender : ISender
    {
        private readonly IEventAggregator eventAggregator = EventAggregatorSingleton.Instance;
        private readonly ISenderWrapper socketWrapper;

        public Sender(string ip)
        {
            socketWrapper = new SocketWrapper(ip);
            eventAggregator.SendMessage(new UpdatesMessage("Server started! Accepting connections...", Visibility.Visible, false));
        }

        public async void Broadcast(string move, int index, bool forward)
        {
            eventAggregator.SendMessage(new UpdatesMessage("Sending Command!", Visibility.Visible, false));
            try
            {
                await socketWrapper.BroadcastAsync(move, index, forward);
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode == SocketError.ConnectionReset)
                {
                    eventAggregator.SendMessage(new UpdatesMessage("A client has disconnected!", Visibility.Hidden, true));
                }
            }

            eventAggregator.SendMessage(new UpdatesMessage("Command Executed!", Visibility.Hidden, true));
        }

        public async Task AcceptClientsAsync()
        {
            try
            {
                while (true)
                {
                    await socketWrapper.AcceptClientsAsync();
                    eventAggregator.SendMessage(new UpdatesMessage("A client connected to the server!", Visibility.Hidden, true));
                }
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode == SocketError.OperationAborted)
                {
                    eventAggregator.SendMessage(new UpdatesMessage("The server has been stoped!", Visibility.Hidden, true));
                    return;
                }

                throw e;
            }
        }

        public void Stop()
        {
            socketWrapper.Disconnect();
        }
    }
}
