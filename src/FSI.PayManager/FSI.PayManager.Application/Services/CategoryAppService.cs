using FSI.PayManager.Application.Dtos;
using FSI.PayManager.Application.Interfaces;
using FSI.PayManager.Domain.Entities;
using FSI.PayManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.PayManager.Application.Services
{
    public sealed class CategoryAppService : ICrudAppService<CategoryDto>
    {
        private readonly IRepository<Category> _repository;

        public CategoryAppService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryDto>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _repository.GetAllAsync(ct);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(id, ct);
            return entity is null ? null : MapToDto(entity);
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto dto, CancellationToken ct = default)
        {
            var entity = new Category(
                userId: dto.UserId,
                name: dto.Name,
                type: dto.Type,
                colorHex: dto.ColorHex,
                isSystem: dto.IsSystem
            );

            var created = await _repository.AddAsync(entity, ct);
            return MapToDto(created);
        }

        public async Task<CategoryDto?> UpdateAsync(int id, CategoryDto dto, CancellationToken ct = default)
        {
            var existing = await _repository.GetByIdAsync(id, ct);
            if (existing is null)
                return null;

            existing.Update(
                name: dto.Name,
                type: dto.Type,
                colorHex: dto.ColorHex,
                isSystem: dto.IsSystem,
                isActive: dto.IsActive
            );

            var updated = await _repository.UpdateAsync(existing, ct);
            return MapToDto(updated);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            await _repository.DeleteAsync(id, ct);
        }

        private static CategoryDto MapToDto(Category entity)
        {
            return new CategoryDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Name = entity.Name,
                Type = entity.Type,
                ColorHex = entity.ColorHex,
                IsSystem = entity.IsSystem,
                IsActive = entity.IsActive
            };
        }
    }
}