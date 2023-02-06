using JwtAuthenticationManager.Model;
using Microsoft.IdentityModel.Token;
namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "dc04f652-ce54-4d53-8fbf-028e7c732e9f";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<UserAccount> _users;
        public JwtTokenHandler()
        {
            __users = new List<UserAccount>
            {
                new UserAccount{UserName = "admin", PassWord = "admin123", Role = "Administrator"},
                new UserAccount{UserName = "user01", PassWord = "user01", Role = "User"}
            };
        }
        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request){
            if (string.IsNullOrWhiteSpace(request.UserName)||string.IsNullOrWhiteSpace(request.PassWord)){
                return null;
            }
            var userAccount = _users.Where(x=> x.UserName == request.UserName && x.PassWord == request.PassWord).FisrtOrDefault();
            if (userAccount == null) {
                return null;
            }
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new CLaimIdentity(new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Name, request.UserName),
                new Claim(ClaimTypes.Role, userAccount.Role)
            });
            var signingCredentials = new SigningCredentials(new SymetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityDescriptor
            {
                Subject = claimsIdentity,
                tokenExpiryTimeStamp = tokenExpiryTimeStamp,
                signingCredentials = signingCredentials
            };
            var jwtSecurityToeknHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityToeknHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityToeknHandler.WriteToken(securityToken);
            return new AuthenticationResponse{
                UserName = userAccount.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };
        }
    }
}