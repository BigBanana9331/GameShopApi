using System.ComponentModel.DataAnnotations;

namespace GameShop.User.Contract
{
    public record RegisterUserRequest(
        [Required] string UserName,
        [Required][EmailAddress] string Email,
        [Required] string Password
    );
}