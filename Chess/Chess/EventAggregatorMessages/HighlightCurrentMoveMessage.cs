namespace Chess.EventAggregatorMessages
{
    public class HighlightCurrentMoveMessage
    {
        public HighlightCurrentMoveMessage(bool status)
        {
            Status = status;
        }

        public bool Status { get; set; }
    }
}
