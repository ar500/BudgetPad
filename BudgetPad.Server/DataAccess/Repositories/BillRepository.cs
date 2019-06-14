using BudgetPad.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Server.DataAccess.Repositories
{
    public interface IBillRepository : IGenericRepository<Bill>
    {
        IList<Payment> GetPayments(Guid billId);
    }

    public class BillRepository : GenericRepository<Bill>, IBillRepository
    {
        public BillRepository(BudgetPadContext context, ILogger<BillRepository> logger)
            : base(context, logger)
        {

        }

        public IList<Payment> GetPayments(Guid billId)
        {
            return base.GetAllDirectNav(true).Select(p => p.Payments).FirstOrDefault();
        }
    }
}
