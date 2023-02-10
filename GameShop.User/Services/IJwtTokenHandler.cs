using GameShop.User.Entities;

namespace GameShop.User.Services
{
    public interface IJwtTokenHandler
    {
        public string CreateToken(UserAccount user);
        public string CreateRefeshToken();

    }
}