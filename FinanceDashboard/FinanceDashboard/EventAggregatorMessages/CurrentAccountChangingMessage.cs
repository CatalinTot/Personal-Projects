using FinanceDashboard.ViewModel.Models;

namespace FinanceDashboard.EventAggregatorMessages
{
    public class CurrentAccountChangingMessage
    {
        public CurrentAccountChangingMessage(AccountModel accountModel)
        {
            AccoutModel = accountModel;
        }

        public AccountModel AccoutModel { get; }
    }
}
