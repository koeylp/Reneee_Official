namespace Reneee.Application.Models.Identity
{
    public class JwtSettings
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int AccessTokenExpirationMinutes { get; set; }
        public int? RefreshTokenExpirationDays { get; set; }
        public string? PrivateKeyPath { get; set; }
        public string? PublicKeyPath { get; set; }
    }
}
