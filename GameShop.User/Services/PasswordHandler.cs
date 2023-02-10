namespace GameShop.User.Services
{
    public class PasswordHandler : IPasswordHandler
    {
        public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            passwordHash = BCrypt.Net.BCrypt.HashPassword(password, passwordSalt);
            // Console.WriteLine(passwordSalt);
            // Console.WriteLine(passwordHash);
        }

        public bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            
            var enteredPasswordHash = BCrypt.Net.BCrypt.HashPassword(password, passwordSalt);
            // Console.WriteLine(enteredPasswordHash);
            return BCrypt.Net.BCrypt.Verify(enteredPasswordHash, passwordHash);
            // return enteredPasswordHash == passwordHash;
        }
    }
}