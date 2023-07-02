using System.ComponentModel.DataAnnotations;

namespace GameShop.User.Contract
{
    public record UpdateUserRequest(
        string UserName,

        [EmailAddress] string Email,

        [StringLength(10)] string Password,

        string Role,

        string PhoneNumber,

        string AvatarPath
    );
}