namespace Chess.EventAggregatorMessages
{
    public class DisableConnectClientButtonMessage
    {
        public DisableConnectClientButtonMessage(bool isConnected)
        {
            IsConnected = isConnected;
        }

        public bool IsConnected { get; }
    }
}
