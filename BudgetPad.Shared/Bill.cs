using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BudgetPad.Shared
{
    public class Bill : ExpenseBase
    {
        [Required]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; } 

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPlanned { get; set; }

        [MaxLength(50)]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        public string PayoutAccountNumber { get; set; }
    }
}
