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
    public sealed class ReminderAppService : ICrudAppService<ReminderDto>
    {
        private readonly IRepository<Reminder> _repository;

        public ReminderAppService(IRepository<Reminder> repository)
        {
            _repository = repository;
        }

        public async Task<List<ReminderDto>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _repository.GetAllAsync(ct);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ReminderDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(id, ct);
            return entity is null ? null : MapToDto(entity);
        }

        public async Task<ReminderDto> CreateAsync(ReminderDto dto, CancellationToken ct = default)
        {
            var entity = new Reminder(
                userId: dto.UserId,
                financialTransactionId: dto.FinancialTransactionId,
                daysBeforeDue: dto.DaysBeforeDue
            );

            if (dto.IsSent && dto.SentAt.HasValue)
            {
                entity.MarkAsSent(dto.SentAt.Value);
            }

            var created = await _repository.AddAsync(entity, ct);
            return MapToDto(created);
        }

        public async Task<ReminderDto?> UpdateAsync(int id, ReminderDto dto, CancellationToken ct = default)
        {
            var existing = await _repository.GetByIdAsync(id, ct);
            if (existing is null)
                return null;

            // Não temos update completo na entidade, só marca envio
            if (!existing.IsSent && dto.IsSent && dto.SentAt.HasValue)
            {
                existing.MarkAsSent(dto.SentAt.Value);
            }

            var updated = await _repository.UpdateAsync(existing, ct);
            return MapToDto(updated);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            await _repository.DeleteAsync(id, ct);
        }

        private static ReminderDto MapToDto(Reminder entity)
        {
            return new ReminderDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                FinancialTransactionId = entity.FinancialTransactionId,
                DaysBeforeDue = entity.DaysBeforeDue,
                IsSent = entity.IsSent,
                SentAt = entity.SentAt,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}