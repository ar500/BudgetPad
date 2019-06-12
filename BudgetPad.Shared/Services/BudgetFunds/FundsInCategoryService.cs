using System.Collections.Generic;
using System.Linq;

namespace BudgetPad.Shared.Services.BudgetFunds
{
    public class FundsInCategoryService : IFundsInCategoryService
    {
        public IEnumerable<FundsInCategoryModel> GroupExpensesByCat(IEnumerable<Bill> bills)
        {
            if (!bills.Any() || bills == null)
            {
                return null;
            }

            var groupedCategories = bills
                .GroupBy(e => e.BudgetCategory)
                .Where(g => g.Key != null)
                .Select(g => new FundsInCategoryModel
                {
                    Category = g.FirstOrDefault().BudgetCategory.ShortName,
                    TotalFunds = g.Sum(e => e.AmountPlanned)
                }).ToList();

            return groupedCategories;
        }
    }
}
