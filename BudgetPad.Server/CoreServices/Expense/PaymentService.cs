using System;
using System.Threading.Tasks;
using BudgetPad.Server.DataAccess.Repositories;
using BudgetPad.Shared;
using Microsoft.Extensions.Logging;

namespace BudgetPad.Server.CoreServices.Expense
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IExpenseLoggerService _expenseLoggerService;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(ILogger<PaymentService> logger, 
            IExpenseLoggerService expenseLoggerService,
            IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _expenseLoggerService = expenseLoggerService;
            _paymentRepository = paymentRepository;
        }


        public async Task<Payment> AddPaymentAsync(Bill expense, Payment payment, string remarks = null)
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

        public async Task DeletePaymentAsync(Guid id)
        {
            await _paymentRepository.Delete(id);
            await _expenseLoggerService.DeleteExpenseLog(id);
        }

        public async Task<Payment> UpdatePaymentAsync(Guid id, Payment payment, string remarks = null)
        {
            await _paymentRepository.Update(id, payment);
            if (!string.IsNullOrWhiteSpace(remarks))
            {
                await _expenseLoggerService.UpdateExpenseLogRemarks(id, remarks);
            }

            return payment;
        }

        public async Task<Payment> GetPaymentById(Guid paymentId)
        {
            return await _paymentRepository.GetById(paymentId, true);
        }
    }
}
