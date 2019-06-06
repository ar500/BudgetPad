using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BudgetPad.Shared.Dtos
{
    public class BudgetForCreateDto
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CycleStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CycleEndDate { get; set; }

        [Required]
        [Range(0, 10000)]
        [DataType(DataType.Currency)]
        public decimal AllottedFunds { get; set; } = 0;

        [Required]
        public IEnumerable<String> ReturnedBillNames { get; set; } = new List<string>();

        [Required]
        [DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; } = "fixThis@Soon.Com";

        public IList<BillDto> Bills { get; set; } = new List<BillDto>();
    }
}
