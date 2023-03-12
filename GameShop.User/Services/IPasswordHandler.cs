namespace GameShop.User.Services
{
    public interface IPasswordHandler
    {
        public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt);
        public bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt);

    }
}