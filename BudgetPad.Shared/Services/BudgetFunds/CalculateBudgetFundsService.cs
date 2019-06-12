using System.Collections.Generic;
using System.Linq;

namespace BudgetPad.Shared.Services.BudgetFunds
{
    public class CalculateBudgetFundsService : ICalculateBudgetFundsService
    {
        public decimal CalculateAllocatedFunds(IEnumerable<FundsInCategoryModel> categoryGroups)
        {
            if (categoryGroups == null)
            {
                return 0;
            }

            if (!categoryGroups.Any())
            {
                return 0;
            }

            return categoryGroups
                .Select(g => g.TotalFunds)
                .Sum();
        }

        public decimal CalculateUnallocateFunds(decimal allocatedFunds, decimal allottedFunds)
        {
            if (allottedFunds <= allocatedFunds)
            {
                return 0;
            }

            return allottedFunds - allocatedFunds;
        }
    }
}
