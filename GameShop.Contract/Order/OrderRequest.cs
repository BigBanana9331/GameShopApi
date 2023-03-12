using System.ComponentModel.DataAnnotations;
namespace GamesShop.Contract.Order
{
    public record OrderRequest(
        [Required] Guid UserId,
        decimal Discounted,
        // decimal Total,
        DateOnly PurchasedDate
    );
}