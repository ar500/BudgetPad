using BudgetPad.Shared;
using System;
using System.Collections.Generic;

namespace BudgetPad.Server.DataAccess.Repositories
{
    public interface IBillRepository : IGenericRepository<Bill>
    {
        IList<Payment> GetPayments(Guid billId);
        IList<Bill> GetBillsFromNameList(IEnumerable<string> billNames);
        IList<Bill> GetBillsFromIdList(IEnumerable<Guid> billIds);
    }
}
