namespace GamesShop.Contract.Order
{
    public record OrderItemRequest(
        Guid OrderId,
        Guid GameId,
        int Quantity,
        decimal PurchasedPrice
    );
}