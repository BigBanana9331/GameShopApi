namespace GameShop.Common.Settings
{
    public class JwtSettings
    {
        public string? SecretKey { get; init; }
        public int ExpiryMinutes { get; init; }
        public string? Issuer { get; init; }
        public string[]? Audiences { get; init; }
    }
}