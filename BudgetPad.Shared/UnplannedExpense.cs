using System.ComponentModel.DataAnnotations;

namespace BudgetPad.Shared
{
    public class UnplannedExpense : ExpenseBase, IEntity
    {
        [Required]
        [MaxLength(200)]
        public string Remarks { get; set; }

        [Required]
        public Payment Payment { get; set; }
    }
}
