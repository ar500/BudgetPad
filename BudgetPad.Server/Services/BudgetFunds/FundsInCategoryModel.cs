using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Server.Services.BudgetFunds
{
    public class FundsInCategoryModel
    {
        public string Category { get; set; }

        public decimal TotalFunds { get; set; }
    }
}
