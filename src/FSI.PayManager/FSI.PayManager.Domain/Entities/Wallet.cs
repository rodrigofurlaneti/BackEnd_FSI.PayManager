namespace FSI.PayManager.Domain.Entities
{
    public sealed class Wallet : BaseEntity
    {
        public int UserId { get; private set; }
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }
        public decimal InitialBalance { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDefault { get; private set; }

        public User User { get; private set; } = default!;
        public ICollection<FinancialTransaction> Transactions { get; private set; } = new List<FinancialTransaction>();

        private Wallet() { }

        public Wallet(int userId, string name, string? description, decimal initialBalance, bool isDefault = false)
        {
            UserId = userId;
            Name = name;
            Description = description;
            InitialBalance = initialBalance;
            CreatedAt = DateTime.UtcNow;
            IsDefault = isDefault;
        }

        public void Update(string name, string? description, decimal initialBalance, bool isDefault)
        {
            Name = name;
            Description = description;
            InitialBalance = initialBalance;
            IsDefault = isDefault;
        }
    }
}