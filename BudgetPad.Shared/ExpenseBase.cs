using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetPad.Shared
{
    public class ExpenseBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public IList<Payment> Payments { get; set; } = new List<Payment>();

        public Guid? BudgetId { get; set; }

        [Required]
        public BudgetCategory BudgetCategory { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime EntryDateTime { get; set; }

        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte[] RowVersion { get; set; }
    }
}
