using System.ComponentModel.DataAnnotations;

namespace GameShop.User.Contract
{
    public record LoginResponse(
        string? Token,
        string? RefreshToken,
        bool Result
    );
}