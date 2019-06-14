using BudgetPad.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Server.DataAccess.Repositories
{
    public interface IExpenseRepositry : IGenericRepository<ExpenseLogEntry>
    {
        ExpenseLogEntry GetLogByPayment(Guid paymentId);
    }
    public class ExpenseRepository : GenericRepository<ExpenseLogEntry>, IExpenseRepositry
    {
        public ExpenseRepository(BudgetPadContext context, ILogger<ExpenseRepository> logger)
            : base(context, logger)
        {

        }

        public ExpenseLogEntry GetLogByPayment(Guid paymentId)
        {
            return base.GetAllDirectNav(true).FirstOrDefault(p => p.Payment.Id == paymentId);
        }
    }
}
