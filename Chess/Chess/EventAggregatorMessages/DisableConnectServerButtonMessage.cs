namespace Chess.EventAggregatorMessages
{
    public class DisableConnectServerButtonMessage
    {
        public DisableConnectServerButtonMessage(bool isConnected)
        {
            IsConnected = isConnected;
        }

        public bool IsConnected { get; }
    }
}
