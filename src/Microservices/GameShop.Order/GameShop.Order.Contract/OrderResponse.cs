namespace GamesShop.Order.Contract
{
    public record OrderResponse(
        Guid Id,
        Guid UserId,
        decimal Discounted,
        DateOnly PurchasedDate
    );
}