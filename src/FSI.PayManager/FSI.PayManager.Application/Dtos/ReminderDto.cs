namespace FSI.PayManager.Application.Dtos
{
    public sealed class ReminderDto : IHasId
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FinancialTransactionId { get; set; }
        public int DaysBeforeDue { get; set; }
        public bool IsSent { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}