using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BudgetPad.Shared
{
    public class ExpenseLogEntry : ExpenseBase
    {
        [MaxLength(200)]
        public string Remarks { get; set; }
    }
}
