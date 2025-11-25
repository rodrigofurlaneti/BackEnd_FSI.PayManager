namespace FSI.PayManager.Application.Dtos
{
    public sealed class LoginResponseDto
    {
        public string AccessToken { get; set; } = default!;
        public DateTime ExpiresAtUtc { get; set; }

        public int UserId { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
