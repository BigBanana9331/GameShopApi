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
    private readonly IRepository<UserAcount> _userRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public UsersController(IRepository<UserAcount> userRepository, IPublishEndpoint publishEndpoint)
    {
        _userRepository = userRepository;
        _publishEndpoint = publishEndpoint;
    }
    [HttpPost]
    public async Task<ActionResult<UserAcount>> CreateGameAsync(UserRequest request)
    {
        var user = MapUserRequest(request);
        await _userRepository.CreateAsync(user);
        await _publishEndpoint.Publish(new UserCreated(
                user.Id
            ));
        return CreatedAtAction(
            nameof(GetUserAsync),
            new { id = user.Id },
            user
        );
    }
    [HttpGet]
    public async Task<IEnumerable<UserResponse>> GetUsersAsync()
    {
        var users = (await _userRepository.GetAllAsync()).Select(user => MapUserResponse(user));
        return users;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUserAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var response = MapUserResponse(user);
        return response;
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync(Guid id, UserRequest request)
    {
        var existingUser = await _userRepository.GetAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }
        var user = MapUserRequest(request, id);
        await _userRepository.UpdateAsync(user);
        // await _publishEndpoint.Publish(new UserUpdated(
        //         user.Id,
        //         user.UserName,
        //         user.Email
        //     ));
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var existingUser = await _userRepository.GetAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }
        await _userRepository.RemoveAsync(id);
        await _publishEndpoint.Publish(new UserDeleted(
                id
            ));
        return NoContent();
    }

    private UserAcount MapUserRequest(UserRequest request, Guid? id = null)
    {
        return UserAcount.Create(request.UserName, request.Email, request.PhoneNumber, request.Password, request.AvatarPath, id);
    }

    private UserResponse MapUserResponse(UserAcount user)
    {
        return new UserResponse(user.Id, user.UserName, user.Email, user.PhoneNumber, user.Password, user.AvatarPath);
    }
}
