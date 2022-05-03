using System;
using System.Threading.Tasks;

namespace Chess.Network
{
    public interface ISender
    {
        public void Broadcast(string move, int index, bool forward);

        public void Stop();

        public Task AcceptClientsAsync();
    }
}
