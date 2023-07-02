using GameShop.User.Entities;

namespace GameShop.User.Services
{
    public interface IJwtTokenHandler
    {
        public Token CreateToken(UserAccount user);
        public string CreateRefeshToken();

    }
}