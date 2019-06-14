using BudgetPad.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Server.DataAccess.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAllDirectNav(bool eager = false);

        Task<TEntity> GetById(Guid id, bool eager = false);

        Task Create(TEntity entity);

        Task Update(Guid id, TEntity entity);

        Task<bool> EntryExists(Guid id);

        Task Delete(Guid id);
    }
}
