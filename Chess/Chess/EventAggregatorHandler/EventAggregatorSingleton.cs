namespace Chess.EventAggregatorHandler
{
    public sealed class EventAggregatorSingleton
    {
        private static readonly EventAggregator EventInstance = new ();

        static EventAggregatorSingleton()
        {
        }

        private EventAggregatorSingleton()
        {
        }

        public static EventAggregator Instance
        {
            get
            {
                return EventInstance;
            }
        }
    }
}
