using BudgetPad.Shared;
using System;

namespace BudgetPad.Server.DataAccess.Repositories
{
    public interface IExpenseRepositry : IGenericRepository<ExpenseLogEntry>
    {
        ExpenseLogEntry GetLogByPayment(Guid paymentId);
    }
}
