using Microsoft.AspNetCore.Mvc;
using GameShop.Common;
using MassTransit;
using GameShop.User.Entities;
using GameShop.Contract.User;

namespace GameShop.User.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IRepository<UserAccount> _userRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public UsersController(IRepository<UserAccount> userRepository, IPublishEndpoint publishEndpoint)
    {
        _userRepository = userRepository;
        _publishEndpoint = publishEndpoint;
    }
    [HttpPost]
    public async Task<ActionResult<UserAccount>> CreateUserAsync(UserRequest request)
    {
        var user = UserAccount.MapUserRequest(request);
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
    [HttpGet]
    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
    {
        var users = (await _userRepository.GetAllAsync()).Select(user => UserAccount.MapUserResponse(user));
        return users;
    }
    [HttpGet("{userId}")]
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
    public async Task<IActionResult> UpdateUserAsync(Guid userId, UserRequest request)
    {
        var existingUser = await _userRepository.GetAsync(userId);
        if (existingUser == null)
        {
            return NotFound();
        }
        var user = UserAccount.MapUserRequest(request, userId);
        await _userRepository.UpdateAsync(user);
        await _publishEndpoint.Publish(new UserUpdated(
                user.Id,
                user.UserName,
                user.Email
            ));
        return NoContent();
    }
    [HttpDelete("{userId}")]
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
}
