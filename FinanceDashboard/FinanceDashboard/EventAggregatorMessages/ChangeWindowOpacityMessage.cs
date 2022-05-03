using System.Windows;

namespace FinanceDashboard.EventAggregatorMessages
{
    public class ChangeWindowOpacityMessage
    {
        public ChangeWindowOpacityMessage(Visibility visibility)
        {
            Visibility = visibility;
        }

        public Visibility Visibility { get; }
    }
}
