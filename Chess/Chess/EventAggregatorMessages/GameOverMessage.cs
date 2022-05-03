namespace Chess.EventAggregatorMessages
{
    public class GameOverMessage
    {
        public GameOverMessage(bool status)
        {
            Status = status;
        }

        public bool Status { get; }
    }
}
