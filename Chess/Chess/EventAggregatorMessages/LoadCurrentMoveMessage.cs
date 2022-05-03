namespace Chess.EventAggregatorMessages
{
    public class LoadCurrentMoveMessage
    {
        public LoadCurrentMoveMessage(bool forward)
        {
            Forward = forward;
        }

        public bool Forward { get; set; }
    }
}
