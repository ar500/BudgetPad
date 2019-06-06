using BudgetPad.Shared;
using System.Collections.Generic;

namespace BudgetPad.Server.Services.BudgetFunds
{
    public interface IFundsInCategoryService
    {
        IEnumerable<FundsInCategoryModel> GroupExpensesByCat(IEnumerable<Bill> budgets);
    }
}