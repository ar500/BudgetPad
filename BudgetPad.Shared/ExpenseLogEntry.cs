using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetPad.Shared
{
    public class ExpenseLogEntry 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Payment Payment { get; set; }

        [Required]
        public Guid ExpenseId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime EntryDateTime { get; set; }

        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte[] RowVersion { get; set; }

        [MaxLength(200)]
        public string Remarks { get; set; }
    }
}
