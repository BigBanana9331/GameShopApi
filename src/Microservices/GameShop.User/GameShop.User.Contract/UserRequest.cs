using System.ComponentModel.DataAnnotations;

namespace GameShop.User.Contract
{
    public record UserRequest(
        [Required] string UserName,

        [Required][EmailAddress] string Email,

        [Required][StringLength(10)] string Password,

        string Role,

        string PhoneNumber,

        string AvatarPath
    );
}