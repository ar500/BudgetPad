using System;
using System.Threading.Tasks;
using BudgetPad.Shared;
using Microsoft.Extensions.Logging;

namespace BudgetPad.Server.CoreServices.Expense
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IExpenseLoggerService _expenseLoggerService;

        public PaymentService(ILogger<PaymentService> logger, IExpenseLoggerService expenseLoggerService)
        {
            _logger = logger;
            _expenseLoggerService = expenseLoggerService;
        }


        public async Task<Payment> AddPaymentAsync<T>(T expense, Payment payment, string remarks = null) where T : ExpenseBase
        {
            try
            {
                expense.Payments.Add(payment);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError("Null reference exception encountered when adding payment to expense.");
                return null;
            }

            await _expenseLoggerService.LogExpense(expense, remarks);

            return payment;
        }

    }
}
