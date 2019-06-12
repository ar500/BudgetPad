using BudgetPad.Shared.Services.BudgetFunds;
using System.Collections.Generic;
using System.Linq;

namespace BudgetPad.Shared.Dtos.DtoExtensions
{
    public class BudgetDtoExtension : BudgetDto
    {
        public IEnumerable<FundsInCategoryModel> CategoryGroups { get; set; }

        public decimal AllocatedFunds
        {
            get => CalculateAllocatedFunds();
        }

        public decimal UnallocatedFunds
        {
            get => CalculateUnallocateFunds();
        }

        public decimal CalculateAllocatedFunds()
        {
            if (CategoryGroups == null)
            {
                return 0;
            }

            if (!CategoryGroups.Any())
            {
                return 0;
            }

            return CategoryGroups
                .Select(g => g.TotalFunds)
                .Sum();
        }

        public decimal CalculateUnallocateFunds()
        {
            return base.AllottedFunds - AllocatedFunds;
        }
    }
}
