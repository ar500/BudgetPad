using System.ComponentModel.DataAnnotations;

namespace BudgetPad.Shared
{
    public class ExpenseLogEntry : ExpenseBase
    {
        [MaxLength(200)]
        public string Remarks { get; set; }
    }
}
