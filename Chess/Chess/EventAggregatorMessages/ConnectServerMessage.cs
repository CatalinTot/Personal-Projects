namespace Chess.EventAggregatorMessages
{
    public class ConnectServerMessage
    {
        public ConnectServerMessage(string ip)
        {
            Ip = ip;
        }

        public string Ip { get; }
    }
}
