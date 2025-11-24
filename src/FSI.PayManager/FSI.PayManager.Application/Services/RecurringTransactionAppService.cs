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
    public sealed class RecurringTransactionAppService : ICrudAppService<RecurringTransactionDto>
    {
        private readonly IRepository<RecurringTransaction> _repository;

        public RecurringTransactionAppService(IRepository<RecurringTransaction> repository)
        {
            _repository = repository;
        }

        public async Task<List<RecurringTransactionDto>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _repository.GetAllAsync(ct);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<RecurringTransactionDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(id, ct);
            return entity is null ? null : MapToDto(entity);
        }

        public async Task<RecurringTransactionDto> CreateAsync(RecurringTransactionDto dto, CancellationToken ct = default)
        {
            var entity = new RecurringTransaction(
                userId: dto.UserId,
                walletId: dto.WalletId,
                categoryId: dto.CategoryId,
                title: dto.Title,
                description: dto.Description,
                amount: dto.Amount,
                transactionType: dto.TransactionType,
                frequency: dto.Frequency,
                dayOfMonth: dto.DayOfMonth,
                dayOfWeek: dto.DayOfWeek,
                startDate: dto.StartDate,
                endDate: dto.EndDate,
                nextOccurrenceDate: dto.NextOccurrenceDate
            );

            var created = await _repository.AddAsync(entity, ct);
            return MapToDto(created);
        }

        public async Task<RecurringTransactionDto?> UpdateAsync(int id, RecurringTransactionDto dto, CancellationToken ct = default)
        {
            var existing = await _repository.GetByIdAsync(id, ct);
            if (existing is null)
                return null;

            existing.Update(
                title: dto.Title,
                description: dto.Description,
                amount: dto.Amount,
                transactionType: dto.TransactionType,
                frequency: dto.Frequency,
                dayOfMonth: dto.DayOfMonth,
                dayOfWeek: dto.DayOfWeek,
                startDate: dto.StartDate,
                endDate: dto.EndDate,
                nextOccurrenceDate: dto.NextOccurrenceDate,
                isActive: dto.IsActive
            );

            var updated = await _repository.UpdateAsync(existing, ct);
            return MapToDto(updated);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            await _repository.DeleteAsync(id, ct);
        }

        private static RecurringTransactionDto MapToDto(RecurringTransaction entity)
        {
            return new RecurringTransactionDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                WalletId = entity.WalletId,
                CategoryId = entity.CategoryId,
                Title = entity.Title,
                Description = entity.Description,
                Amount = entity.Amount,
                TransactionType = entity.TransactionType,
                Frequency = entity.Frequency,
                DayOfMonth = entity.DayOfMonth,
                DayOfWeek = entity.DayOfWeek,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                NextOccurrenceDate = entity.NextOccurrenceDate,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}