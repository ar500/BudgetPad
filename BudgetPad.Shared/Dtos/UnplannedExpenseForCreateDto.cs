using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BudgetPad.Shared.Dtos
{
    public class UnplannedExpenseForCreateDto
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Remarks { get; set; }

        [Required]
        public PaymentForCreateDto Payment { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public BudgetCategoryDto BudgetCategory { get; set; }

        public Guid? BudgetId { get; set; }
    }
}
