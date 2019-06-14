using BudgetPad.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Server.DataAccess.Repositories
{
    public interface IBudgetRepository : IGenericRepository<Budget>
    {
        IQueryable<Budget> GetBudgets();

        Task<Budget> GetBudget(Guid id);
    }
}
