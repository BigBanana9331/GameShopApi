using System.ComponentModel.DataAnnotations;
namespace GamesShop.Order.Contract
{
    public record OrderItemRequest(
        [Required] Guid OrderId,
        [Required] Guid GameId,
        [Range(0, 1000)] int Quantity,
        [Range(0, 10000000)] decimal PurchasedPrice
    );
}