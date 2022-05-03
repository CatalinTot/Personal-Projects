namespace Chess.EventAggregatorMessages
{
    public class EndOfMovesMessage
    {
        public EndOfMovesMessage(bool isReachedLowerLimit, bool isReachedUpperLimit)
        {
            IsReachedLowerLimit = isReachedLowerLimit;
            IsReachedUpperLimit = isReachedUpperLimit;
        }

        public bool IsReachedLowerLimit { get; set; }

        public bool IsReachedUpperLimit { get; set; }
    }
}
