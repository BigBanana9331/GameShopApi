using System.ComponentModel.DataAnnotations;

namespace GameShop.Contract.User
{
    public record LoginRequest(
        [Required] string UserName,
        [Required] string Password
    );
}