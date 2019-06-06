using BudgetPad.Shared;

namespace BudgetPad.Server.Services.Expense
{
    public interface IExpenseService
    {
        decimal AddPayment(ExpenseBase expense, decimal payment);
        decimal PayInFull(Bill bill);
    }
}