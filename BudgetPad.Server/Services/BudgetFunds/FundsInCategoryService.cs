using BudgetPad.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Server.Services.BudgetFunds
{
    public class FundsInCategoryService : IFundsInCategoryService
    {
        public IEnumerable<FundsInCategoryModel> GroupExpensesByCat(IEnumerable<Bill> budgets)
        {
            if (!budgets.Any() || budgets == null)
            {
                return null;
            }

            var groupedCategories = budgets
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
