namespace FinanceDashboard.EventAggregatorMessages
{
    public class ChangeCurrentAccountMessage
    {
        public ChangeCurrentAccountMessage(string accountName)
        {
            AccountName = accountName;
        }

        public string AccountName { get; set; }
    }
}
