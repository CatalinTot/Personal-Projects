using FinanceDashboard.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDashboard.ViewModel
{
    public class PlanExpandViewModel
    {
        private readonly PlanModel model;

        public PlanExpandViewModel(PlanModel model)
        {
            this.model = model;
        }
    }
}
