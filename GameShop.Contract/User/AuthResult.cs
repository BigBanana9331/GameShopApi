using System.ComponentModel.DataAnnotations;

namespace GameShop.Contract.User
{
    public record AuthResult(
        string? Token,
        string? RefreshToken,
        bool Result,
        string? msg
    );
}