using System.Collections.Generic;

namespace BudgetPad.Shared.Services.BudgetFunds
{
    public interface IFundsInCategoryService
    {
        IEnumerable<FundsInCategoryModel> GroupExpensesByCat(IEnumerable<Bill> bills);
    }
}