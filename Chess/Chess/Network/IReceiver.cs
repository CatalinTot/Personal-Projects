namespace Chess.Network
{
    public interface IReceiver
    {
        public void ConnectTo(string ip);

        public void Stop();
    }
}
