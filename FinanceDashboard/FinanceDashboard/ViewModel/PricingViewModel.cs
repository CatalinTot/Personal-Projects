using FinanceDashboard.ViewModel.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Windows.Input;

namespace FinanceDashboard.ViewModel
{
    public class PricingViewModel : ViewModelBase
    {
        private readonly IEnumerable<PlanModel> items;
        private PlanModel selectedPlan;
        private ICommand expandPlanDetailsCommand;
        private bool isExpandedDetails;
        private ICommand compressPlanDetailsCommand;

        public PricingViewModel()
        {
            System.Resources.ResourceManager manager = new ("FinanceDashboard.Localization.Strings", Assembly.GetExecutingAssembly());
            var text = manager.GetString("Plans", CultureInfo.CurrentCulture);
            items = JsonSerializer.Deserialize<List<PlanModel>>(text);
            selectedPlan = items.FirstOrDefault();
        }

        public IEnumerable<PlanModel> Items { get => items; }

        public PlanModel SelectedPlan
        {
            get => selectedPlan;
            set
            {
                selectedPlan = value;
                OnPropertyChanged(nameof(SelectedPlan));
            }
        }

        public bool IsExpandedDetails
        {
            get => isExpandedDetails;
            set
            {
                isExpandedDetails = value;
                OnPropertyChanged(nameof(IsExpandedDetails));
            }
        }

        public ICommand ExpandPlanDetailsCommand => expandPlanDetailsCommand ??= new RelayCommand(param => Expand(param));

        public ICommand CompressPlanDetailsCommand => compressPlanDetailsCommand ??= new RelayCommand(param => Compress());

        private void Expand(object param)
        {
            IsExpandedDetails = true;
            SelectedPlan = Items.First(x => x.Title == param.ToString());
        }

        private void Compress()
        {
            IsExpandedDetails = false;
        }
    }
}
