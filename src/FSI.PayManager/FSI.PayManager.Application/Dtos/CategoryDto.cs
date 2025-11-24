namespace FSI.PayManager.Application.Dtos
{
    public sealed class CategoryDto : IHasId
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!; // Income / Expense
        public string? ColorHex { get; set; }
        public bool IsSystem { get; set; }
        public bool IsActive { get; set; }
    }
}