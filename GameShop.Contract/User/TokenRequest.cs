using System.ComponentModel.DataAnnotations;

namespace GameShop.Contract.User
{
    public record TokenRequest(
        [Required] string Token,
        [Required] string RefreshToken
    );
}