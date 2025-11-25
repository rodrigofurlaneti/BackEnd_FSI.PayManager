namespace FSI.PayManager.Application.Security
{
    public sealed class JwtSettings
    {
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string Secret { get; set; } = default!;
        public int AccessTokenMinutes { get; set; }
    }
}
