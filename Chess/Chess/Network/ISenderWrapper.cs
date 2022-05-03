using System.Threading.Tasks;

namespace Chess.Network
{
    public interface ISenderWrapper
    {
        public Task BroadcastAsync(string move, int index, bool forward);

        public void Disconnect();

        public Task AcceptClientsAsync();
    }
}
