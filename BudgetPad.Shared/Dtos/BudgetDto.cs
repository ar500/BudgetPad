using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetPad.Shared.Dtos
{
    public class BudgetDto
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CycleStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CycleEndDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AllottedFunds { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; }

        public ICollection<BillDto> Bills { get; set; }

        public ICollection<UnplannedExpenseDto> UnplannedExpenses { get; set; }
    }
}
