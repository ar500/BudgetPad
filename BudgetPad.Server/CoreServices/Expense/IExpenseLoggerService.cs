using System;
using System.Threading.Tasks;
using BudgetPad.Shared;

namespace BudgetPad.Server.CoreServices.Expense
{
    public interface IExpenseLoggerService
    {
        Task<ExpenseLogEntry> LogExpense<T>(T expense, string remarks = null) where T : ExpenseBase;

        Task<ExpenseLogEntry> DeleteExpenseLog(Guid paymentId);

        Task<ExpenseLogEntry> UpdateExpenseLogRemarks(Guid paymentId, string remarks);
    }
}