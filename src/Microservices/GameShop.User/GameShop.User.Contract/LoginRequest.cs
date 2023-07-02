using System.ComponentModel.DataAnnotations;

namespace GameShop.User.Contract
{
    public record LoginRequest(
        [Required] string UserName,
        [Required] string Password
    );
}