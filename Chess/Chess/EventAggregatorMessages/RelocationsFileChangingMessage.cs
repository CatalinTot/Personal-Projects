using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.EventAggregatorMessages
{
    public class RelocationsFileChangingMessage
    {
        public RelocationsFileChangingMessage(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
