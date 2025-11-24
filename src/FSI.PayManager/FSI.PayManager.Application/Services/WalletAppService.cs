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
    public sealed class WalletAppService : ICrudAppService<WalletDto>
    {
        private readonly IRepository<Wallet> _repository;

        public WalletAppService(IRepository<Wallet> repository)
        {
            _repository = repository;
        }

        public async Task<List<WalletDto>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _repository.GetAllAsync(ct);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<WalletDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(id, ct);
            return entity is null ? null : MapToDto(entity);
        }

        public async Task<WalletDto> CreateAsync(WalletDto dto, CancellationToken ct = default)
        {
            var entity = new Wallet(
                userId: dto.UserId,
                name: dto.Name,
                description: dto.Description,
                initialBalance: dto.InitialBalance,
                isDefault: dto.IsDefault
            );

            var created = await _repository.AddAsync(entity, ct);
            return MapToDto(created);
        }

        public async Task<WalletDto?> UpdateAsync(int id, WalletDto dto, CancellationToken ct = default)
        {
            var existing = await _repository.GetByIdAsync(id, ct);
            if (existing is null)
                return null;

            existing.Update(
                name: dto.Name,
                description: dto.Description,
                initialBalance: dto.InitialBalance,
                isDefault: dto.IsDefault
            );

            var updated = await _repository.UpdateAsync(existing, ct);
            return MapToDto(updated);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            await _repository.DeleteAsync(id, ct);
        }

        private static WalletDto MapToDto(Wallet entity)
        {
            return new WalletDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Name = entity.Name,
                Description = entity.Description,
                InitialBalance = entity.InitialBalance,
                IsDefault = entity.IsDefault,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}