using System.Collections.Generic;

namespace Chess.EventAggregatorMessages
{
    public class InitRelocationsCollectionMessage
    {
        public InitRelocationsCollectionMessage(string[] message)
        {
            Message = message;
        }

        public IEnumerable<string> Message { get; }
    }
}
