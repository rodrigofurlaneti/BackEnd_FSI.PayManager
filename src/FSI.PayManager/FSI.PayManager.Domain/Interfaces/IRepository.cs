using FSI.PayManager.Domain.Entities;
using System.Linq.Expressions;

namespace FSI.PayManager.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}