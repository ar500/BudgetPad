using BudgetPad.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Server.DataAccess.Repositories
{
    public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
    {
        private readonly BudgetPadContext _context;

        public BudgetRepository(BudgetPadContext context, ILogger<BudgetRepository> logger)
            : base(context, logger)
        {
            _context = context;
        }

        public async Task<Budget> GetBudget(Guid id)
        {
            return await GetBudgets().FirstOrDefaultAsync(b => b.Id == id);
        }

        public IQueryable<Budget> GetBudgets()
        {
            return _context.Budgets
                .Include(b => b.Bills)
                .ThenInclude(c => c.BudgetCategory)
                .AsQueryable();
        }
    }
}
