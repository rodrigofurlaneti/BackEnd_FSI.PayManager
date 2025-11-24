namespace FSI.PayManager.Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        public int UserId { get; private set; }
        public string Name { get; private set; } = default!;
        public string Type { get; private set; } = default!; // Income / Expense
        public string? ColorHex { get; private set; }
        public bool IsSystem { get; private set; }
        public bool IsActive { get; private set; }

        private Category() { }

        public Category(int userId, string name, string type, string? colorHex, bool isSystem = false)
        {
            UserId = userId;
            Name = name;
            Type = type;
            ColorHex = colorHex;
            IsSystem = isSystem;
            IsActive = true;
        }

        public void Update(string name, string type, string? colorHex, bool isSystem, bool isActive)
        {
            Name = name;
            Type = type;
            ColorHex = colorHex;
            IsSystem = isSystem;
            IsActive = isActive;
        }
    }
}