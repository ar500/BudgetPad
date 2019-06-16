using BudgetPad.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Server.DataAccess.Repositories
{
    public class UnplannedExpenseRepository : GenericRepository<UnplannedExpense>, IUnplannedExpenseRepository
    {
        public UnplannedExpenseRepository(BudgetPadContext context, ILogger<UnplannedExpenseRepository> logger)
            : base(context, logger)
        {
        }
    }
}
