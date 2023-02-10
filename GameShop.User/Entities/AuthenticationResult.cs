namespace GameShop.User.Entities
{
    public class AuthenticationResult
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public bool Result { get; set; }
    }

}