using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetPad.Shared.Dtos
{
    public class PaymentForCreateDto
    {
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePaid { get; set; }
    }
}
