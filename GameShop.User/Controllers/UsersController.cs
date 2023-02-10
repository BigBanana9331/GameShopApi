using Microsoft.AspNetCore.Mvc;
using GameShop.Common;
using MassTransit;
using GameShop.User.Entities;
using GameShop.Contract.User;
using GameShop.User.Services;
using Microsoft.AspNetCore.Authorization;
namespace GameShop.User.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IRepository<UserAccount> _userRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IConfiguration _configuration;
    private readonly IJwtTokenHandler _jwtTokenHandler;
    private readonly IPasswordHandler _passwordHandler;
    public UsersController
    (
        IRepository<UserAccount> userRepository, 
        IPublishEndpoint publishEndpoint, 
        IConfiguration configuration,
        IJwtTokenHandler jwtTokenHandler,
        IPasswordHandler passwordHandler
    )
    {
        _userRepository = userRepository;
        _publishEndpoint = publishEndpoint;
        _configuration = configuration;
        _jwtTokenHandler = jwtTokenHandler;
        _passwordHandler = passwordHandler;
    }

    [HttpPost] //post /users
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<UserAccount>> CreateUserAsync(UserRequest request)
    {
        _passwordHandler.CreatePasswordHash(request.Password, out string passwordSalt, out string passwordHash);
        var user = UserAccount.MapUserRequest(request, passwordHash, passwordSalt);
        await _userRepository.CreateAsync(user);
        await _publishEndpoint.Publish(new UserCreated(
                user.Id
            ));
        return CreatedAtAction(
            nameof(GetUserByIdAsync),
            new { id = user.Id },
            user
        );
    }

    [HttpGet] // get /users
    [Authorize(Roles = "Administrator")]
    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
    {
        var users = (await _userRepository.GetAllAsync()).Select(user => UserAccount.MapUserResponse(user));
        return users;
    }

    [HttpGet("{userId}")] // get /users/{userId}
    [Authorize(Roles = "Administrator")]
    [Authorize(Policy = "PersionalPolicy")]
    public async Task<ActionResult<UserResponse>> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        var response = UserAccount.MapUserResponse(user);
        return response;
    }

    [HttpPut("{userId}")]
    [Authorize(Roles = "Administrator")]
    [Authorize(Policy = "PersionalPolicy")]
    public async Task<IActionResult> UpdateUserAsync(Guid userId, UserRequest request)
    {
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
    public async Task<ActionResult<UserAccount>> Login(LoginRequest request)
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
        return Ok(token);
    }



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
