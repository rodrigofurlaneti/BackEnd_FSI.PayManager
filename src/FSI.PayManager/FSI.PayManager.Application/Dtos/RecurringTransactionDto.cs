namespace FSI.PayManager.Application.Dtos
{
    public sealed class RecurringTransactionDto : IHasId
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WalletId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = default!;
        public string Frequency { get; set; } = default!;
        public byte? DayOfMonth { get; set; }
        public byte? DayOfWeek { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime NextOccurrenceDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}