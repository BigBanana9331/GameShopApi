using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GameShop.Common.Settings;
using GameShop.User.Entities;
using Microsoft.IdentityModel.Tokens;

namespace GameShop.User.Services
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly IConfiguration _configuration;

        public JwtTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateRefeshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public Token CreateToken(UserAccount user)
        {
            var jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes);

            var tokenKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Aud, string.Join(", ",jwtSettings.Audiences)),
                new Claim(JwtRegisteredClaimNames.Iss, jwtSettings.Issuer),
                // new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),
                new Claim("ID", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims);

            var signingCredentials = new SigningCredentials
            (
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
            );

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            var accessToken = jwtSecurityTokenHandler.WriteToken(securityToken);

            var token = new Token
            {
                Id = securityToken.Id,
                AccessToken = accessToken
            };

            return token;
        }
        // public void ValidateToken(string Token, TokenValidationParameters param, out SecurityToken validatedToken){
        //     var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        //     jwtSecurityTokenHandler.ValidateToken()
        // }
    }
}