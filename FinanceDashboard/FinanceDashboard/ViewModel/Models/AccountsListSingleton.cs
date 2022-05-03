using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text.Json;

namespace FinanceDashboard.ViewModel.Models
{
    public sealed class AccountsListSingleton
    {
        private static readonly List<AccountModel> AccountsInstance = PrepareAccounts();

        static AccountsListSingleton()
        {
        }

        private AccountsListSingleton()
        {
        }

        public static List<AccountModel> Instance
        {
            get
            {
                return AccountsInstance;
            }
        }

        private static List<AccountModel> PrepareAccounts()
        {
            System.Resources.ResourceManager manager = new ("FinanceDashboard.Localization.Strings", Assembly.GetExecutingAssembly());
            var text = manager.GetString("Countries", CultureInfo.CurrentCulture);
            return JsonSerializer.Deserialize<List<AccountModel>>(text);
        }
    }
}
