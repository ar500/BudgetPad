using System;
using System.Threading.Tasks;
using BudgetPad.Shared;

namespace BudgetPad.Server.CoreServices.Expense
{
    public interface IPaymentService
    {
        Task<Payment> AddPaymentAsync<T>(T expense, Payment payment, string remarks = null) where T : ExpenseBase;

        Task DeletePaymentAsync(Guid id);

        Task<Payment> UpdatePaymentAsync(Guid id, Payment payment, string remarks = null);

        Task<Payment> GetPaymentById(Guid paymentId);
    }
}