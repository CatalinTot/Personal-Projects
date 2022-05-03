using Chess.EventAggregatorHandler;
using FinanceDashboard.EventAggregatorMessages;
using FinanceDashboard.ViewModel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FinanceDashboard.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IEventAggregator eventAggregator;
        private ICommand showAccountsCommand;
        private AccountModel selectedAccount;

        public DashboardViewModel()
        {
            eventAggregator = EventAggregatorSingleton.Instance;
            selectedAccount = AccountsListSingleton.Instance.FirstOrDefault();
            eventAggregator.RegisterHandler<ChangeCurrentAccountMessage>(AccountChangingHandler);
        }

        public IEnumerable<AccountModel> Accounts { get; }

        public AccountModel SelectedAccount
        {
            get => selectedAccount;
            set
            {
                selectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));
            }
        }

        public ICommand ShowAccountsCommand
        {
            get
            {
                showAccountsCommand ??= new RelayCommand(param => ShowAccountsList());
                return showAccountsCommand;
            }
        }

        private void ShowAccountsList()
        {
            eventAggregator.SendMessage(new ChangeWindowOpacityMessage(Visibility.Visible));
        }

        private void AccountChangingHandler(ChangeCurrentAccountMessage message)
        {
            SelectedAccount = AccountsListSingleton.Instance.FirstOrDefault(x => x.Name == message.AccountName);
        }
    }
}
