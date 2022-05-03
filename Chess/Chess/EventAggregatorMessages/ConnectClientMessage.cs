namespace Chess.EventAggregatorMessages
{
    public class ConnectClientMessage
    {
        public ConnectClientMessage(string ip)
        {
            Ip = ip;
        }

        public string Ip { get; }
    }
}
