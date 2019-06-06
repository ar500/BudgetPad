using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Shared.Dtos
{
    public class BudgetCategoryDto
    {
        public Guid Id { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        // TODO: Create BillDto
        //public ICollection<Bill> budgets { get; set; }
    }
}
