using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetPad.Shared.Dtos
{
    public class ExpenseBaseForCreateDto
    {
        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePaid { get; set; }

        [DataType(DataType.Currency)]
        public decimal AmountSpent { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public BudgetCategoryDto BudgetCategory { get; set; }

        public Guid? BudgetId { get; set; }
    }
}