using System.ComponentModel.DataAnnotations;

namespace GameShop.Contract.User
{
    public record UserRequest(
        [Required] string UserName,
        string Email
    );
}