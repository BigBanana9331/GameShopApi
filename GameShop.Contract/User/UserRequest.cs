using System.ComponentModel.DataAnnotations;

namespace GameShop.Contract.User
{
    public record UserRequest(
        [Required] string UserName,
        [EmailAddress]string Email,
        [StringLength(10)] string PhoneNumber,
        [Required]string Password,
        string AvatarPath
    );
}