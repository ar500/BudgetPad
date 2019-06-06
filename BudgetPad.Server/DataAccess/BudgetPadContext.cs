using BudgetPad.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Server.DataAccess
{
    public class BudgetPadContext : DbContext
    {
        public DbSet<Budget> Budgets { get; set; }

        public DbSet<BudgetCategory> Categories { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<UnplannedExpense> UnplannedExpenses { get; set; }

        public DbSet<ExpenseLogEntry> ExpenseEntries { get; set; }

        public BudgetPadContext(DbContextOptions<BudgetPadContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseBase>()
                .Property(e => e.EntryDateTime)
                .HasDefaultValueSql("getdate()");
        }
    }
}
