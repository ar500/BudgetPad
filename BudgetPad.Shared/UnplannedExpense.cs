using System.ComponentModel.DataAnnotations;

namespace BudgetPad.Shared
{
    public class UnplannedExpense : ExpenseBase
    {
        [Required]
        [MaxLength(200)]
        public string Remarks { get; set; }

        [Required]
        public Payment Payment { get; set; }
    }
}
