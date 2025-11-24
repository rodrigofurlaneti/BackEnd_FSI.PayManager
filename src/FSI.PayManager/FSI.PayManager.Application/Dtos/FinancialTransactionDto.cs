using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.PayManager.Application.Dtos
{
    public sealed class FinancialTransactionDto : IHasId
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WalletId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = default!;
        public DateTime? DueDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public string Status { get; set; } = default!;
        public bool IsRecurring { get; set; }
        public int? RecurringTransactionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}