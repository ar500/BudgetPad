using System.Collections.Generic;

namespace BudgetPad.Shared.Services.BudgetFunds
{
    public interface ICalculateBudgetFundsService
    {
        decimal CalculateAllocatedFunds(IEnumerable<FundsInCategoryModel> categoryGroups);
        decimal CalculateUnallocateFunds(decimal allocatedFunds, decimal allotedFunds);
    }
}