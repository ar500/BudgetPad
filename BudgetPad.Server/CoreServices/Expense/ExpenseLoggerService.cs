using BudgetPad.Server.DataAccess;
using BudgetPad.Shared;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using BudgetPad.Server.DataAccess.Repositories;
using System;

namespace BudgetPad.Server.CoreServices.Expense
{
    public class ExpenseLoggerService : IExpenseLoggerService
    {
        private readonly IExpenseRepositry _expenseRepositry;
        private readonly ILogger<ExpenseLoggerService> _logger;

        public ExpenseLoggerService(IExpenseRepositry expenseRepositry, ILogger<ExpenseLoggerService> logger)
        {
            _expenseRepositry = expenseRepositry;
            _logger = logger;
        }

        public async Task<ExpenseLogEntry> LogExpense(Bill expense, string remarks = null)
        {
            var paymentToLog = expense.Payments
                .OrderByDescending(e => e.EntryDateTime)
                .FirstOrDefault();

            if(paymentToLog == null)
            {
                return null;
            }

            var logEntry = new ExpenseLogEntry
            {
                Payment = paymentToLog,
                ExpenseId = expense.Id
            };

            if (remarks != null)
            {
                logEntry.Remarks = remarks;
            }

            _logger.LogInformation("A new expense payment was logged.");

            await _expenseRepositry.Create(logEntry);

            return logEntry;
        }

        public async Task<ExpenseLogEntry> DeleteExpenseLog(Guid paymentId)
        {
            var expenseToDelete = _expenseRepositry.GetLogByPayment(paymentId);
            if(expenseToDelete == null)
            {
                _logger.LogWarning($"Payment with id {paymentId} was not found in the database.");
                return null;
            }

            await _expenseRepositry.Delete(expenseToDelete.Id);

            return expenseToDelete;
        }

        public async Task<ExpenseLogEntry> UpdateExpenseLogRemarks(Guid paymentId, string remarks)
        {
            var expenseToUpdate = _expenseRepositry.GetLogByPayment(paymentId);
            expenseToUpdate.Remarks = remarks;
            await _expenseRepositry.Update(expenseToUpdate.Id, expenseToUpdate);
            return expenseToUpdate;
        }
    }
}
