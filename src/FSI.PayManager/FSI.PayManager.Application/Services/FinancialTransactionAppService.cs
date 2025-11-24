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
    public sealed class FinancialTransactionAppService : ICrudAppService<FinancialTransactionDto>
    {
        private readonly IRepository<FinancialTransaction> _repository;

        public FinancialTransactionAppService(IRepository<FinancialTransaction> repository)
        {
            _repository = repository;
        }

        public async Task<List<FinancialTransactionDto>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _repository.GetAllAsync(ct);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<FinancialTransactionDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(id, ct);
            return entity is null ? null : MapToDto(entity);
        }

        public async Task<FinancialTransactionDto> CreateAsync(FinancialTransactionDto dto, CancellationToken ct = default)
        {
            var entity = new FinancialTransaction(
                userId: dto.UserId,
                walletId: dto.WalletId,
                categoryId: dto.CategoryId,
                title: dto.Title,
                description: dto.Description,
                amount: dto.Amount,
                transactionType: dto.TransactionType,
                dueDate: dto.DueDate,
                status: dto.Status,
                isRecurring: dto.IsRecurring,
                recurringTransactionId: dto.RecurringTransactionId
            );

            if (dto.PaidDate.HasValue && dto.Status == "Paid")
            {
                entity.MarkPaid(dto.PaidDate.Value);
            }

            var created = await _repository.AddAsync(entity, ct);
            return MapToDto(created);
        }

        public async Task<FinancialTransactionDto?> UpdateAsync(int id, FinancialTransactionDto dto, CancellationToken ct = default)
        {
            var existing = await _repository.GetByIdAsync(id, ct);
            if (existing is null)
                return null;

            existing.Update(
                title: dto.Title,
                description: dto.Description,
                amount: dto.Amount,
                transactionType: dto.TransactionType,
                dueDate: dto.DueDate,
                paidDate: dto.PaidDate,
                status: dto.Status,
                isRecurring: dto.IsRecurring,
                recurringTransactionId: dto.RecurringTransactionId
            );

            var updated = await _repository.UpdateAsync(existing, ct);
            return MapToDto(updated);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            await _repository.DeleteAsync(id, ct);
        }

        private static FinancialTransactionDto MapToDto(FinancialTransaction entity)
        {
            return new FinancialTransactionDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                WalletId = entity.WalletId,
                CategoryId = entity.CategoryId,
                Title = entity.Title,
                Description = entity.Description,
                Amount = entity.Amount,
                TransactionType = entity.TransactionType,
                DueDate = entity.DueDate,
                PaidDate = entity.PaidDate,
                Status = entity.Status,
                IsRecurring = entity.IsRecurring,
                RecurringTransactionId = entity.RecurringTransactionId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}