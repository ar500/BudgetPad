using BudgetPad.Server.DataAccess;
using BudgetPad.Shared;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AutoMapper;

namespace BudgetPad.Server.CoreServices.Expense
{
    public class ExpenseLoggerService : IExpenseLoggerService
    {
        private readonly BudgetPadContext _context;
        private readonly ILogger<ExpenseLoggerService> _logger;

        public ExpenseLoggerService(BudgetPadContext context, ILogger<ExpenseLoggerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ExpenseLogEntry> LogExpense<T>(T expense, string remarks = null) where T : ExpenseBase
        {
            var logEntry = Mapper.Map<ExpenseLogEntry>(expense);

            if(logEntry == null)
            {
                return null;
            }

            if (remarks != null)
            {
                logEntry.Remarks = remarks;
            }

            _logger.LogInformation("A new expense payment was logged.");

            await _context.ExpenseLogs
                .AddAsync(logEntry);

            await _context.SaveChangesAsync();

            return logEntry;
        }


    }
}
