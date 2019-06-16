using System;
using System.Threading.Tasks;
using BudgetPad.Shared;

namespace BudgetPad.Server.CoreServices.Expense
{
    public interface IPaymentService
    {
        Task<Payment> AddBillPaymentAsync(Bill expense, Payment payment, string remarks = null);

        Task<Payment> AddUnplannedExpensePaymentAsync(UnplannedExpense expense);

        Task DeletePaymentAsync(Guid id);

        Task<Payment> UpdatePaymentAsync(Guid id, Payment payment, string remarks = null);

        Task<Payment> GetPaymentById(Guid paymentId);
    }
}