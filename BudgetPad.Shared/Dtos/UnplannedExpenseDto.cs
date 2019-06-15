using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetPad.Shared.Dtos
{
    public class UnplannedExpenseDto
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Remarks { get; set; }

        [Required]
        public PaymentDto Payment { get; set; }

        public Guid? BudgetId { get; set; }

        [Required]
        public BudgetCategory BudgetCategory { get; set; }
    }
}