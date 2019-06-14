using BudgetPad.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Server.DataAccess.Repositories
{
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

        public IList<Bill> GetBillsFromNameList(IEnumerable<string> billNames)
        {
            return base.GetAllDirectNav(true)
                .Where(e => billNames.Contains(e.ShortName))
                .ToList();
        }

        public IList<Bill> GetBillsFromIdList(IEnumerable<Guid> billIds)
        {
            return base.GetAllDirectNav(true)
                .Where(bill => billIds.Contains(bill.Id))
                .ToList();
        }
    }
}
