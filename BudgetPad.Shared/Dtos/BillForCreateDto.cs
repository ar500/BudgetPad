﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetPad.Shared.Dtos
{
    public class BillForCreateDto
    {
        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePaid { get; set; }

        [DataType(DataType.Currency)]
        public decimal AmountSpent { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public BudgetCategoryDto BudgetCategory { get; set; }

        public Guid? BudgetId { get; set; }

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
        public decimal AmountPlanned { get; set; }

        [MaxLength(50)]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        public string PayoutAccountNumber { get; set; }
    }
}
