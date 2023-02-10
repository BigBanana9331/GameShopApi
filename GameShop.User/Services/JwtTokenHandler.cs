using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            throw new NotImplementedException();
        }

        public string CreateToken(UserAccount user)
        {
            var jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(jwtSettings.ExpiryMinutes);
            var tokenKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
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
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return token;

            // List<Claim> claims = new List<Claim>{
            //     new Claim(ClaimTypes.Name, user.UserName)
            // };
            // var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JwtSettings.SecretKey));
            // var cred = new SigningCredentials(key, SecurityAlgorithms.EcdsaSha256Signature);
            // var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddHours(1), signingCredentials: cred);
            // var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            // return jwt;
        }
    }
}