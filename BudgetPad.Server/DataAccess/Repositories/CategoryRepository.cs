using BudgetPad.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Server.DataAccess.Repositories
{
    public class CategoryRepository : GenericRepository<BudgetCategory>, ICategoryRepository
    {
        public CategoryRepository(BudgetPadContext context, ILogger<CategoryRepository> logger)
            : base(context, logger)
        {

        }

        public BudgetCategory GetCategoryByName(string name)
        {
            return base.GetAllDirectNav(false).FirstOrDefault(c => c.ShortName == name);
        }
    }
}
