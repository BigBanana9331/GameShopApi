using Microsoft.AspNetCore.Mvc;
using GameShop.Common;
using MassTransit;
using GameShop.User.Entities;
using GameShop.Contract.User;
using GameShop.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using GameShop.Common.Settings;

namespace GameShop.User.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IRepository<UserAccount> _userRepository;
    private readonly IRepository<RefreshToken> _tokenRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IConfiguration _configuration;
    private readonly IJwtTokenHandler _jwtTokenHandler;
    private readonly IPasswordHandler _passwordHandler;
    // private readonly TokenValidationParameters _tokenValidationParameters;
    public UsersController
    (
        IRepository<UserAccount> userRepository,
        IRepository<RefreshToken> tokenRepository,
        IPublishEndpoint publishEndpoint,
        IConfiguration configuration,
        IJwtTokenHandler jwtTokenHandler,
        IPasswordHandler passwordHandler
    // TokenValidationParameters tokenValidationParameters
    )
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _publishEndpoint = publishEndpoint;
        _configuration = configuration;
        _jwtTokenHandler = jwtTokenHandler;
        _passwordHandler = passwordHandler;
        // _tokenValidationParameters = tokenValidationParameters;
    }

    [HttpPost] //post /users
    // [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<UserAccount>> CreateUserAsync(UserRequest request)
    {
        _passwordHandler.CreatePasswordHash(request.Password, out string passwordHash, out string passwordSalt);
        var user = UserAccount.MapUserRequest(request, passwordHash, passwordSalt);
        await _userRepository.CreateAsync(user);
        await _publishEndpoint.Publish(new UserCreated(
                user.Id
            ));
        return Ok();
        // return CreatedAtAction(
        //     nameof(GetUserByIdAsync),
        //     new { id = user.Id },
        //     user
        // );
    }

    [HttpGet] // get /users
    // [Authorize(Roles = "Administrator")]
    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
    {
        var users = (await _userRepository.GetAllAsync()).Select(user => UserAccount.MapUserResponse(user));
        return users;
    }

    [HttpGet("{userId}")] // get /users/{userId}
    // [Authorize(Roles = "Administrator")]
    // [Authorize(Policy = "PersionalPolicy")]
    [Authorize]
    public async Task<ActionResult<UserResponse>> GetUserByIdAsync(Guid userId)
    {
        // var currentUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value;
        // Console.WriteLine(currentUser);
        var user = await _userRepository.GetAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        var response = UserAccount.MapUserResponse(user);
        return response;
    }

    [HttpPut("{userId}")]
    // [Authorize(Roles = "Administrator")]
    // [Authorize(Policy = "PersionalPolicy")]
    [Authorize]
    public async Task<IActionResult> UpdateUserAsync(Guid userId, UserRequest request)
    {
        // var id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value;
        // Console.WriteLine(id);
        var existingUser = await _userRepository.GetAsync(userId);
        if (existingUser == null)
        {
            return NotFound();
        }
        _passwordHandler.CreatePasswordHash(request.Password, out string passwordHash, out string passwordSalt);
        var user = UserAccount.MapUserRequest(request, passwordHash, passwordSalt, userId);
        await _userRepository.UpdateAsync(user);
        await _publishEndpoint.Publish(new UserUpdated(
                user.Id,
                user.UserName,
                user.Email
            ));
        return NoContent();
    }

    [HttpDelete("{userId}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteUserAsync(Guid userId)
    {
        var existingUser = await _userRepository.GetAsync(userId);
        if (existingUser == null)
        {
            return NotFound();
        }
        await _userRepository.RemoveAsync(userId);
        await _publishEndpoint.Publish(new UserDeleted(
                userId
            ));
        return NoContent();
    }


    [HttpPost("register")]
    public async Task<ActionResult<UserAccount>> Register(RegisterUserRequest request)
    {
        _passwordHandler.CreatePasswordHash(request.Password, out string passwordHash, out string passwordSalt);
        var user = UserAccount.MapRegisterUserRequest(request, passwordHash, passwordSalt);
        await _userRepository.CreateAsync(user);
        return Ok(user);
    }
    [HttpPost("login")]
    public async Task<ActionResult<AuthResult>> Login(LoginRequest request)
    {
        var user = (await _userRepository.GetAllAsync()).Where(user => user.UserName == request.UserName).FirstOrDefault();
        if (user == null)
        {
            return BadRequest("Null");
        }
        if (!_passwordHandler.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("UserName or Password not match");
        }
        var token = _jwtTokenHandler.CreateToken(user);
        var refreshToken = _jwtTokenHandler.CreateRefeshToken();
        var tokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            // AccessToken = token.AccessToken,
            // RefreshToken = refreshToken,
            Token = refreshToken,
            JwtId = token.Id,
            UserId = user.Id,
            IsUsed = false,
            IsRevoked = false,
            IssuedAt = DateTime.UtcNow,
            ExpiredAt = DateTime.UtcNow.AddHours(1)
        };
        await _tokenRepository.CreateAsync(tokenEntity);
        var result = new AuthResult
        (
            token.AccessToken,
            refreshToken,
            true,
            string.Empty
        );
        return Ok(result);
    }
    [HttpPost("renew-token")]
    public async Task<IActionResult> RenewToken(TokenRequest request)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        var param = new TokenValidationParameters
        {

            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            // ValidIssuer = jwtSettings.Issuer,
            // ValidAudiences = jwtSettings.Audiences,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            // ClockSkew = TimeSpan.Zero
        };

        try
        {
            var tokenInVerification = jwtTokenHandler.ValidateToken(request.Token, param, out var validatedToken);
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var validateResult = jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase
                );
                if (!validateResult)
                {
                    return BadRequest("Invalid Token!");
                }

            }
            var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);
            Console.WriteLine(expiryDate);
            if (expiryDate > DateTime.Now)
            {
                return BadRequest("Token Expired!");
            }
            var storedToken = await _tokenRepository.GetAsync(x => x.Token == request.RefreshToken);
            if (storedToken == null)
            {
                return BadRequest("Refresh Token does not exist!");
            }
            if (storedToken.IsUsed)
            {
                return BadRequest("Refresh Token is used!");
            }
            if (storedToken.IsRevoked)
            {
                return BadRequest("Refresh Token is revoke");
            }
            var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (storedToken.JwtId != jti)
            {
                return BadRequest("Token not mactch!");
            }
            if (storedToken.ExpiredAt < DateTime.UtcNow)
            {
                return BadRequest("Refeshtoken Expired!");
            }
            storedToken.IsRevoked = true;
            storedToken.IsUsed = true;
            await _tokenRepository.UpdateAsync(storedToken);
            var user = await _userRepository.GetAsync(storedToken.UserId);
            var token = _jwtTokenHandler.CreateToken(user);
            var refreshToken = _jwtTokenHandler.CreateRefeshToken();
            var tokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                // AccessToken = token.AccessToken,
                // RefreshToken = refreshToken,
                Token = refreshToken,
                JwtId = token.Id,
                UserId = user.Id,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1)
            };
            await _tokenRepository.CreateAsync(tokenEntity);
            var result = new AuthResult
            (
                token.AccessToken,
                refreshToken,
                true,
                string.Empty
            );
            return Ok(result);
        }
        catch (System.Exception)
        {

            return BadRequest("Something wen't wrong!");
        }
    }

    private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
        return dateTimeVal;
    }


    // [HttpPost("renew-token")]
    // public async Task<ActionResult<AuthResult>> RefreshToken(TokenRequest request)
    // {
    //     // var refreshToken = (await _tokenRepository.GetAllAsync()).Select(t => t.Token.Equals(request.RefreshToken)&& t.IsUsed ==false && t.IsRevoked ==false);
    //     // if 
    //     if (ModelState.IsValid)
    //     {
    //         var result = await VerifyAndGenerateToken(request);
    //         if (result == null)
    //         {
    //             return BadRequest("Invalid Token");
    //         }
    //         return Ok(result);
    //     }
    //     return BadRequest();
    // }

    // private async Task<ActionResult<AuthResult>> VerifyAndGenerateToken(TokenRequest request)
    // {
    //     var jwtTokenHandler = new JwtSecurityTokenHandler();
    //     var jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
    //     // _tokenValidationParameters.ValidateLifetime = false;
    //     var param = new TokenValidationParameters
    //     {
    //         ValidateIssuerSigningKey = true,
    //         ValidateIssuer = true,
    //         ValidateAudience = true,
    //         ValidateLifetime = true,

    //         ValidIssuer = jwtSettings.Issuer,
    //         ValidAudiences = jwtSettings.Audiences,
    //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    //     };
    //     var tokenInVerification = jwtTokenHandler.ValidateToken(request.Token, param, out var validatedToken);
    //     if (validatedToken is JwtSecurityToken jwtSecurityToken)
    //     {
    //         var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
    //         if (result == false)
    //         {
    //             // return new TokenResponse(
    //             //     null,
    //             //     null,
    //             //     false
    //             // );
    //             return BadRequest(new AuthResult
    //             (
    //                 null,
    //                 null,
    //                 false,
    //                 "Something wen't wrong!"
    //             ));
    //         }
    //         var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
    //         var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);
    //         if (expiryDate > DateTime.Now)
    //         {
    //             return BadRequest(new AuthResult
    //             (
    //                 null,
    //                 null,
    //                 false,
    //                 "Token Expired!"
    //             ));
    //         }
    //         var storedToken = await _tokenRepository.GetAsync(x => x.Token == request.RefreshToken);
    //         if (storedToken == null)
    //         {
    //             return BadRequest(new AuthResult
    //             (
    //                 null,
    //                 null,
    //                 false,
    //                 "Invalid Token"
    //             ));
    //         }
    //         if (storedToken.IsUsed)
    //         {
    //             return BadRequest(new AuthResult
    //             (
    //                 null,
    //                 null,
    //                 false,
    //                 "Invalid Token"
    //             ));
    //         }
    //         if (storedToken.IsRevoked)
    //         {
    //             return BadRequest(new AuthResult
    //             (
    //                 null,
    //                 null,
    //                 false,
    //                 "Invalid Token"
    //             ));
    //         }
    //         var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
    //         if (storedToken.JwtId != jti)
    //         {
    //             return BadRequest(new AuthResult
    //             (
    //                 null,
    //                 null,
    //                 false,
    //                 "Invalid Token"
    //             ));
    //         }
    //         if (storedToken.ExpiredAt < DateTime.UtcNow)
    //         {
    //             return BadRequest(new AuthResult
    //             (
    //                 null,
    //                 null,
    //                 false,
    //                 "Token Expired!"
    //             ));
    //         }
    //         storedToken.IsUsed = true;
    //         storedToken.IsRevoked = true;
    //         await _tokenRepository.UpdateAsync(storedToken);
    //         var user = await _userRepository.GetAsync(storedToken.UserId);
    //         var token = _jwtTokenHandler.CreateToken(user);
    //         var refreshToken = _jwtTokenHandler.CreateRefeshToken();
    //         var tokenEntity = new RefreshToken
    //         {
    //             Id = Guid.NewGuid(),
    //             // AccessToken = token.AccessToken,
    //             // RefreshToken = refreshToken,
    //             Token = refreshToken,
    //             JwtId = token.Id,
    //             UserId = user.Id,
    //             IsUsed = false,
    //             IsRevoked = false,
    //             IssuedAt = DateTime.UtcNow,
    //             ExpiredAt = DateTime.UtcNow.AddHours(1)
    //         };
    //         await _tokenRepository.CreateAsync(tokenEntity);
    //         var response = new AuthResult
    //         (
    //             token.AccessToken,
    //             refreshToken,
    //             true,
    //             string.Empty
    //         );
    //         return Ok(response);
    //     }
    //     // return new TokenResponse(
    //     //             null,
    //     //             null,
    //     //             false
    //     // );
    //     return BadRequest(new AuthResult
    //             (
    //                 null,
    //                 null,
    //                 false,
    //                 "Something wen't wrong"
    //             ));
    // }




    // private string CreateToken(UserAccount user)
    // {
    //     List<Claim> claims = new List<Claim>{
    //         new Claim(ClaimTypes.Name, user.UserName)
    //     };
    //     var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JwtSettings.SecretKey));
    //     var cred = new SigningCredentials(key, SecurityAlgorithms.EcdsaSha256Signature);
    //     var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddHours(1), signingCredentials: cred);
    //     var jwt = new JwtSecurityTokenHandler().WriteToken(token);
    //     return jwt;
    // }
    // private string CreateToken(UserAccount user)
    // {
    //     var jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
    //     var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(jwtSettings.ExpiryMinutes);
    //     var tokenKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
    //     var claims = new[]
    //     {
    //         new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
    //         new Claim(JwtRegisteredClaimNames.Name, user.UserName),
    //         new Claim(ClaimTypes.Role, user.Role)
    //     };
    //     var claimsIdentity = new ClaimsIdentity(claims);
    //     var signingCredentials = new SigningCredentials
    //     (
    //         new SymmetricSecurityKey(tokenKey),
    //         SecurityAlgorithms.HmacSha256Signature
    //     );
    //     var securityTokenDescriptor = new SecurityTokenDescriptor()
    //     {
    //         Subject = claimsIdentity,
    //         Expires = tokenExpiryTimeStamp,
    //         SigningCredentials = signingCredentials
    //     };
    //     var jwtSecurityToeknHandler = new JwtSecurityTokenHandler();
    //     var securityToken = jwtSecurityToeknHandler.CreateToken(securityTokenDescriptor);
    //     var token = jwtSecurityToeknHandler.WriteToken(securityToken);
    //     return token;
    // }
    // private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    // {
    //     passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
    //     passwordHash = BCrypt.Net.BCrypt.HashPassword(password, passwordSalt);
    //     Console.WriteLine(passwordSalt);
    //     Console.WriteLine(passwordHash);

    // }
    // private bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
    // {
    //     var enteredPasswordHash = BCrypt.Net.BCrypt.HashPassword(password, passwordSalt);
    //     Console.WriteLine(enteredPasswordHash);
    //     // return BCrypt.Net.BCrypt.Verify(enteredPasswordHash, passwordHash);
    //     return enteredPasswordHash == passwordHash;
    // }
    // private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    // {
    //     using (var hmac = new HMACSHA512())
    //     {
    //         passwordSalt = Convert.ToBase64String(hmac.Key);
    //         passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
    //     }
    // }
    // private bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
    // {
    //     using (var hmac = new HMACSHA512(Convert.FromBase64String(passwordSalt)))
    //     {
    //         var computedHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
    //         return computedHash == passwordHash;
    //     }
    // }
}
