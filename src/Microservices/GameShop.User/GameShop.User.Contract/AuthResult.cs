using System.ComponentModel.DataAnnotations;

namespace GameShop.User.Contract
{
    public record AuthResult(
        string? Token,
        string? RefreshToken,
        bool Result,
        string? msg
    );
}