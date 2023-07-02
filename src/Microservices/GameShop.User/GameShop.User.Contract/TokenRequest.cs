using System.ComponentModel.DataAnnotations;

namespace GameShop.User.Contract
{
    public record TokenRequest(
        [Required] string Token,
        [Required] string RefreshToken
    );
}