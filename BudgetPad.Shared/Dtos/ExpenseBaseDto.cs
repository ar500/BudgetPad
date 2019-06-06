using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BudgetPad.Shared.Dtos
{
    public class ExpenseBaseDto
    {
        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePaid { get; set; }

        [DataType(DataType.Currency)]
        public decimal AmountSpent { get; set; }

        [Required]
        public BudgetCategoryDto BudgetCategory { get; set; }

        public Guid? BudgetId { get; set; }
    }
}
