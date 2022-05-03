using Chess.EventAggregatorHandler;
using FinanceDashboard.EventAggregatorMessages;
using FinanceDashboard.ViewModel.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace FinanceDashboard.ViewModel
{
    public class AccountsListViewModel : ViewModelBase
    {
        private readonly IEventAggregator eventAggregator;
        private ICommand dismissAccountsPopupCommand;
        private ICommand selectAccountCommand;

        public AccountsListViewModel()
        {
            eventAggregator = EventAggregatorSingleton.Instance;
            Accounts = AccountsListSingleton.Instance;
        }

        public IEnumerable<AccountModel> Accounts { get; }

        public ICommand DismissAccountsPopupCommand
        {
            get
            {
                dismissAccountsPopupCommand ??= new RelayCommand(param => Close(param));
                return dismissAccountsPopupCommand;
            }
        }

        public ICommand SelectAccountCommand
        {
            get
            {
                selectAccountCommand ??= new RelayCommand(param => SelectAccount(param));
                return selectAccountCommand;
            }
        }

        private void SelectAccount(object param)
        {
            eventAggregator.SendMessage(new ChangeCurrentAccountMessage(param.ToString()));
            eventAggregator.SendMessage(new ChangeWindowOpacityMessage(Visibility.Hidden));
        }

        private void Close(object param)
        {
            if (param is not Window window)
            {
                return;
            }

            window.Close();
            eventAggregator.SendMessage(new ChangeWindowOpacityMessage(Visibility.Hidden));
        }
    }
}
