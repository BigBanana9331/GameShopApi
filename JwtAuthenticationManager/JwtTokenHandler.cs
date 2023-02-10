using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthenticationManager.Model;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "Sup3rS3cr3tK3yFromSup3rS3cr3t";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<UserAccount> _users;
        public JwtTokenHandler()
        {
            _users = new List<UserAccount>
            {
                new UserAccount{UserName = "admin", PassWord = "admin123", Role = "Administrator"},
                new UserAccount{UserName = "user01", PassWord = "user01", Role = "User"}
            };
        }
        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.PassWord))
            {
                return null;
            }
            var userAccount = _users.Where(x => x.UserName == request.UserName && x.PassWord == request.PassWord).FirstOrDefault();
            if (userAccount == null)
            {
                return null;
            }
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.UTF8.GetBytes(JWT_SECURITY_KEY);
            var claims = new[] 
            {
                new Claim(JwtRegisteredClaimNames.Name, request.UserName),
                new Claim(ClaimTypes.Role, userAccount.Role)
            };
            var claimsIdentity = new ClaimsIdentity(claims);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };
            var jwtSecurityToeknHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityToeknHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityToeknHandler.WriteToken(securityToken);
            return new AuthenticationResponse
            {
                UserName = userAccount.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };
        }
    }
}