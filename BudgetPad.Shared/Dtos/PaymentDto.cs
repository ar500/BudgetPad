using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetPad.Shared.Dtos
{
    public class PaymentDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal AmountPaid { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePaid { get; set; }

        public DateTime EntryDateTime { get; set; }
    }
}
