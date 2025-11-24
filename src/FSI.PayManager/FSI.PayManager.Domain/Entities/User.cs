namespace FSI.PayManager.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public string FullName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public ICollection<Wallet> Wallets { get; private set; } = new List<Wallet>();

        private User() { } 

        public User(string fullName, string email, string passwordHash)
        {
            FullName = fullName;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public void Update(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}