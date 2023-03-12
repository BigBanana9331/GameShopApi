using System.ComponentModel.DataAnnotations;

namespace GameShop.Contract.User
{
    public record LoginResponse(
        string? Token,
        string? RefreshToken,
        bool Result
    );
}