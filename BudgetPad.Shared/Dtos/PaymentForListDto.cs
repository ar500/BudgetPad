using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetPad.Shared.Dtos
{
    public class PaymentForListDto
    {
        public Guid Id { get; set; }

        [DataType(DataType.Currency)]
        public decimal AmountPaid { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePaid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime EntryDateTime { get; set; }
    }
}
