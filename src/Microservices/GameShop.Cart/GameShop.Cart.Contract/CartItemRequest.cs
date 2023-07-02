using System.ComponentModel.DataAnnotations;

namespace GameShop.Cart.Contract
{
    public record CartItemRequest(
        [Required] Guid UserId,
        [Required] Guid GameId,
        [Range(0, 1000)] int Quantity
    );
}
