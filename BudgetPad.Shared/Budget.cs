using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetPad.Shared
{
    public class Budget : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CycleStartDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        public DateTime CycleEndDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AllottedFunds { get; set; } = 0;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; } = "removethis@soonest.opp";

        public ICollection<Bill> Bills { get; set; } = new List<Bill>();

        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte[] RowVersion { get; set; }
    }
}
