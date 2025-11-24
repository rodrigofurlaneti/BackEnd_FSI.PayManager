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
    public sealed class UserAppService : ICrudAppService<UserDto>
    {
        private readonly IRepository<User> _repository;

        public UserAppService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<List<UserDto>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _repository.GetAllAsync(ct);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<UserDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(id, ct);
            return entity is null ? null : MapToDto(entity);
        }

        public async Task<UserDto> CreateAsync(UserDto dto, CancellationToken ct = default)
        {
            var entity = new User(dto.FullName, dto.Email, dto.PasswordHash);
            var created = await _repository.AddAsync(entity, ct);
            return MapToDto(created);
        }

        public async Task<UserDto?> UpdateAsync(int id, UserDto dto, CancellationToken ct = default)
        {
            var existing = await _repository.GetByIdAsync(id, ct);
            if (existing is null)
                return null;

            existing.Update(dto.FullName, dto.Email);
            if (!dto.IsActive)
                existing.Deactivate();
            else
                existing.Activate();

            var updated = await _repository.UpdateAsync(existing, ct);
            return MapToDto(updated);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            await _repository.DeleteAsync(id, ct);
        }

        private static UserDto MapToDto(User entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Email = entity.Email,
                PasswordHash = entity.PasswordHash,
                CreatedAt = entity.CreatedAt,
                IsActive = entity.IsActive
            };
        }
    }
}
