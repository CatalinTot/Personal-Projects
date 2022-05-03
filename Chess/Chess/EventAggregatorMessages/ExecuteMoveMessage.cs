namespace Chess.EventAggregatorMessages
{
    public class ExecuteMoveMessage
    {
        public ExecuteMoveMessage(string move, int index, bool forward)
        {
            Move = move;
            Index = index;
            Forward = forward;
        }

        public string Move { get; set; }

        public int Index { get; set; }

        public bool Forward { get; set; }
    }
}
