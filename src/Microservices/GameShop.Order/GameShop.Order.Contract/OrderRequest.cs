using System.ComponentModel.DataAnnotations;
namespace GamesShop.Order.Contract
{
    public record OrderRequest(
        [Required] Guid UserId,
        decimal Discounted,
        // decimal Total,
        DateOnly PurchasedDate
    );
}