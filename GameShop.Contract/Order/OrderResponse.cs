namespace GamesShop.Contract.Order
{
    public record OrderResponse(
        Guid Id,
        Guid UserId,
        decimal Discounted,
        DateTime PurchasedDate
    );
}