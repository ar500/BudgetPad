using System;

namespace BudgetPad.Shared.Dtos
{
    public class BudgetCategoryDto
    {
        public Guid Id { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }
    }
}
