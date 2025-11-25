using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FSI.PayManager.Domain.Entities;
using FSI.PayManager.Domain.Interfaces;

namespace FSI.PayManager.UnitTests.Domain.Interfaces
{
    public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly List<TEntity> _storage = new();
        private int _idCounter = 1;

        public Task<List<TEntity>> GetAllAsync(CancellationToken ct = default)
            => Task.FromResult(_storage.ToList());

        public Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
            => Task.FromResult(_storage.FirstOrDefault(e => e.Id == id));

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
            => Task.FromResult(_storage.AsQueryable().Where(predicate).ToList());

        public Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
        {
            entity.GetType().GetProperty("Id")!.SetValue(entity, _idCounter++);
            _storage.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            var index = _storage.FindIndex(e => e.Id == entity.Id);
            if (index >= 0)
                _storage[index] = entity;

            return Task.FromResult(entity);
        }

        public Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = _storage.FirstOrDefault(e => e.Id == id);
            if (entity != null)
                _storage.Remove(entity);

            return Task.CompletedTask;
        }
    }
}