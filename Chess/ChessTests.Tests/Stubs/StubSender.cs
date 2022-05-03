using Chess.Network;
using System.Threading.Tasks;

namespace ChessTests.Tests.Stubs
{
    public class StubSender : ISender
    {
        public void Broadcast(string move, int index, bool forward)
        {
            return;
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void Start(string ip)
        {
            throw new System.NotImplementedException();
        }

        public Task AcceptClientsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
