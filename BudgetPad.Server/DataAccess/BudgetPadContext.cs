using BudgetPad.Shared;
using Microsoft.EntityFrameworkCore;

namespace BudgetPad.Server.DataAccess
{
    public class BudgetPadContext : DbContext
    {
        public DbSet<Budget> Budgets { get; set; }

        public DbSet<BudgetCategory> Categories { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<UnplannedExpense> UnplannedExpenses { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<ExpenseLogEntry> ExpenseLogs { get; set; }

        public BudgetPadContext(DbContextOptions<BudgetPadContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .Property(e => e.EntryDateTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<UnplannedExpense>()
                .Property(e => e.EntryDateTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ExpenseLogEntry>()
                .Property(e => e.EntryDateTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Payment>()
                .Property(e => e.EntryDateTime)
                .HasDefaultValueSql("getdate()");
        }
    }
}
