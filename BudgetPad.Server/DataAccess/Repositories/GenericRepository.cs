using BudgetPad.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPad.Server.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly BudgetPadContext _context;
        private readonly ILogger<GenericRepository<TEntity>> _logger;

        public GenericRepository(BudgetPadContext context, ILogger<GenericRepository<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            if (!await SaveAsync())
            {
                _logger.LogError("We encountered an issue while trying to save changes to the database");
            }
        }

        public async Task Delete(Guid id)
        {
            var entry = await GetById(id, true);
            if (entry != null)
            {
                _context.Set<TEntity>().Remove(entry);
                if (!await SaveAsync())
                {
                    _logger.LogError("We encountered an issue while trying to save changes to the database.");
                }
            }
        }

        public virtual IQueryable<TEntity> GetAllDirectNav(bool eager = false)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (eager)
            {
                foreach (var property in _context.Model
                    .FindEntityType(typeof(TEntity))
                    .GetNavigations())
                {
                    query = query.Include(property.Name);
                }
            }
            return query;
        }

        public virtual async Task<TEntity> GetById(Guid id, bool eager = false)
        {
            return await GetAllDirectNav(eager).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(Guid id, TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            if (!await SaveAsync())
            {
                _logger.LogError("We encountered an issue while trying to save changes to the database.");
            }
        }

        public async Task<bool> EntryExists(Guid id)
        {
            return await GetAllDirectNav(false).AnyAsync(e => e.Id == id);
        }

        private async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
