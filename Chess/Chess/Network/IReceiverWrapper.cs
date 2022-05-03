using System.Threading.Tasks;

namespace Chess.Network
{
    public interface IReceiverWrapper
    {
        public Task ConnectAsync(string ip);

        public Task<string> ReceiveCommandAsync();

        public void Disconnect();
    }
}
