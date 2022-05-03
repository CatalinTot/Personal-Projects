using System.Windows;

namespace Chess.EventAggregatorMessages
{
    public class UpdatesMessage
    {
        public UpdatesMessage(string message, Visibility visibility, bool availability)
        {
            Message = message;
            BufferVisibility = visibility;
            Availability = availability;
        }

        public string Message { get; }

        public Visibility BufferVisibility { get; }

        public bool Availability { get; }
    }
}
