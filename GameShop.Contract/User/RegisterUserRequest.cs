using System.ComponentModel.DataAnnotations;

namespace GameShop.Contract.User
{
    public record RegisterUserRequest(
        [Required] string UserName,
        [Required][EmailAddress] string Email,
        [Required] string Password
    );
}