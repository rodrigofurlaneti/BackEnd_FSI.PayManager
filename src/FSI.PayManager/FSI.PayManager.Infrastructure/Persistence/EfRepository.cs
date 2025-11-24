using FSI.PayManager.Domain.Entities;
using FSI.PayManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FSI.PayManager.Infrastructure.Persistence
{
    public sealed class EfRepository<TEntity> : IRepository<TEntity>
       where TEntity : BaseEntity
    {
        private readonly PayManagerDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public EfRepository(PayManagerDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbSet.AsNoTracking().ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _dbSet.FindAsync(new object?[] { id }, ct);
        }

        public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync(ct);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
            await _dbContext.SaveChangesAsync(ct);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync(ct);
            return entity;
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var existing = await GetByIdAsync(id, ct);
            if (existing is null)
                return;

            _dbSet.Remove(existing);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}