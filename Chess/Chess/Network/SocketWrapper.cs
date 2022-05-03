using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Network
{
    public class SocketWrapper : ISenderWrapper, IReceiverWrapper, IDisposable
    {
        private readonly Socket socket;
        private readonly List<Socket> clients = new ();

        public SocketWrapper()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public SocketWrapper(string ip)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse($"{ip}"), 13000));
            socket.Listen();
        }

        public async Task BroadcastAsync(string move, int index, bool forward)
        {
            foreach (var client in clients)
            {
                await client.SendAsync(Encoding.ASCII.GetBytes($"{move}, {index}, {forward}_"), 0);
            }
        }

        public async Task ConnectAsync(string ip)
        {
            await socket.ConnectAsync(new IPEndPoint(IPAddress.Parse($"{ip}"), 13000));
        }

        public async Task<string> ReceiveCommandAsync()
        {
            var buffer = new byte[128];
            var message = string.Empty;
            while (message.IndexOf('_') < 0)
            {
                message += Encoding.ASCII.GetString(buffer, 0, await socket.ReceiveAsync(new ArraySegment<byte>(buffer), 0));
            }

            return message;
        }

        public void Disconnect()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task AcceptClientsAsync()
        {
            clients.Add(await socket.AcceptAsync());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            socket.Dispose();
        }
    }
}
