using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.PayManager.Domain.Entities
{
    public sealed class FinancialTransaction : BaseEntity
    {
        public int UserId { get; private set; }
        public int WalletId { get; private set; }
        public int CategoryId { get; private set; }
        public string Title { get; private set; } = default!;
        public string? Description { get; private set; }
        public decimal Amount { get; private set; }
        public string TransactionType { get; private set; } = default!; // Income / Expense / Transfer
        public DateTime? DueDate { get; private set; }
        public DateTime? PaidDate { get; private set; }
        public string Status { get; private set; } = default!;          // Pending / Paid / Overdue / Canceled
        public bool IsRecurring { get; private set; }
        public int? RecurringTransactionId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Navigation
        public User User { get; private set; } = default!;
        public Wallet Wallet { get; private set; } = default!;
        public Category Category { get; private set; } = default!;
        public RecurringTransaction? RecurringTransaction { get; private set; }
        public ICollection<Reminder> Reminders { get; private set; } = new List<Reminder>();

        private FinancialTransaction() { }

        public FinancialTransaction(
            int userId,
            int walletId,
            int categoryId,
            string title,
            string? description,
            decimal amount,
            string transactionType,
            DateTime? dueDate,
            string status,
            bool isRecurring,
            int? recurringTransactionId)
        {
            UserId = userId;
            WalletId = walletId;
            CategoryId = categoryId;
            Title = title;
            Description = description;
            Amount = amount;
            TransactionType = transactionType;
            DueDate = dueDate;
            Status = status;
            IsRecurring = isRecurring;
            RecurringTransactionId = recurringTransactionId;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkPaid(DateTime paidDate)
        {
            PaidDate = paidDate;
            Status = "Paid";
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(
            string title,
            string? description,
            decimal amount,
            string transactionType,
            DateTime? dueDate,
            DateTime? paidDate,
            string status,
            bool isRecurring,
            int? recurringTransactionId)
        {
            Title = title;
            Description = description;
            Amount = amount;
            TransactionType = transactionType;
            DueDate = dueDate;
            PaidDate = paidDate;
            Status = status;
            IsRecurring = isRecurring;
            RecurringTransactionId = recurringTransactionId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}