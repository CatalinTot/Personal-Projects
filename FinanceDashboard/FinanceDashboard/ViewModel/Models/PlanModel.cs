using System.Collections.Generic;

namespace FinanceDashboard.ViewModel.Models
{
    public class PlanModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Subtitle { get; set; }

        public string Image { get; set; }

        public string Cardcolor { get; set; }

        public IEnumerable<PlanBenefitModel> Benefits { get; set; }
    }
}
