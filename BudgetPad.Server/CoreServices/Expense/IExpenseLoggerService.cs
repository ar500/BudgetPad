using System;
using System.Threading.Tasks;
using BudgetPad.Shared;

namespace BudgetPad.Server.CoreServices.Expense
{
    public interface IExpenseLoggerService
    {
        Task<ExpenseLogEntry> LogBill(Bill expense, string remarks = null);

        Task<ExpenseLogEntry> LogUnplannedExpense(UnplannedExpense expense);

        Task<ExpenseLogEntry> DeleteExpenseLog(Guid paymentId);

        Task<ExpenseLogEntry> UpdateExpenseLogRemarks(Guid paymentId, string remarks);
    }
}