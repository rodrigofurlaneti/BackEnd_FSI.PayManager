using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.PayManager.Domain.Entities
{
    public sealed class Reminder : BaseEntity
    {
        public int UserId { get; private set; }
        public int FinancialTransactionId { get; private set; }
        public int DaysBeforeDue { get; private set; }
        public bool IsSent { get; private set; }
        public DateTime? SentAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User User { get; private set; } = default!;
        public FinancialTransaction FinancialTransaction { get; private set; } = default!;

        private Reminder() { }

        public Reminder(int userId, int financialTransactionId, int daysBeforeDue)
        {
            UserId = userId;
            FinancialTransactionId = financialTransactionId;
            DaysBeforeDue = daysBeforeDue;
            IsSent = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsSent(DateTime sentAt)
        {
            IsSent = true;
            SentAt = sentAt;
        }
    }
}