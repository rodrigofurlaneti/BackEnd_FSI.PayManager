namespace FSI.PayManager.Application.Dtos
{
    public sealed class WalletDto : IHasId
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal InitialBalance { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}