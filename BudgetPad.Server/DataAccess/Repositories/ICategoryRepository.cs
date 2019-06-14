using BudgetPad.Shared;
using System.Threading.Tasks;

namespace BudgetPad.Server.DataAccess.Repositories
{
    public interface ICategoryRepository : IGenericRepository<BudgetCategory>
    {
        BudgetCategory GetCategoryByName(string name);
    }
}
