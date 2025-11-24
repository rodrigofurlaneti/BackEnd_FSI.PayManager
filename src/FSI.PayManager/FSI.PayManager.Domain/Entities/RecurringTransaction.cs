using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.PayManager.Domain.Entities
{
    public sealed class RecurringTransaction : BaseEntity
    {
        public int UserId { get; private set; }
        public int WalletId { get; private set; }
        public int CategoryId { get; private set; }
        public string Title { get; private set; } = default!;
        public string? Description { get; private set; }
        public decimal Amount { get; private set; }
        public string TransactionType { get; private set; } = default!; // Income / Expense
        public string Frequency { get; private set; } = default!;      // Daily, Weekly, Monthly, Yearly
        public byte? DayOfMonth { get; private set; }                  // 1-31
        public byte? DayOfWeek { get; private set; }                   // 1-7
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime NextOccurrenceDate { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Navigation
        public User User { get; private set; } = default!;
        public Wallet Wallet { get; private set; } = default!;
        public Category Category { get; private set; } = default!;
        public ICollection<FinancialTransaction> FinancialTransactions { get; private set; } = new List<FinancialTransaction>();

        private RecurringTransaction() { }

        public RecurringTransaction(
            int userId,
            int walletId,
            int categoryId,
            string title,
            string? description,
            decimal amount,
            string transactionType,
            string frequency,
            byte? dayOfMonth,
            byte? dayOfWeek,
            DateTime startDate,
            DateTime? endDate,
            DateTime nextOccurrenceDate)
        {
            UserId = userId;
            WalletId = walletId;
            CategoryId = categoryId;
            Title = title;
            Description = description;
            Amount = amount;
            TransactionType = transactionType;
            Frequency = frequency;
            DayOfMonth = dayOfMonth;
            DayOfWeek = dayOfWeek;
            StartDate = startDate;
            EndDate = endDate;
            NextOccurrenceDate = nextOccurrenceDate;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(
            string title,
            string? description,
            decimal amount,
            string transactionType,
            string frequency,
            byte? dayOfMonth,
            byte? dayOfWeek,
            DateTime startDate,
            DateTime? endDate,
            DateTime nextOccurrenceDate,
            bool isActive)
        {
            Title = title;
            Description = description;
            Amount = amount;
            TransactionType = transactionType;
            Frequency = frequency;
            DayOfMonth = dayOfMonth;
            DayOfWeek = dayOfWeek;
            StartDate = startDate;
            EndDate = endDate;
            NextOccurrenceDate = nextOccurrenceDate;
            IsActive = isActive;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}