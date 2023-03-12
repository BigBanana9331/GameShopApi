using GameShop.Common;

namespace GameShop.User.Entities
{
    public class RefreshToken :IEntity
    {
        public Guid Id { get; set; }
        public string? Token { get; set; }
        // public string? AccessToken { get; set; }
        // public string? RefreshToken { get; set; }
        public string? JwtId { get; set; }
        public Guid UserId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}