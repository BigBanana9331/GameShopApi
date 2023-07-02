namespace GamesShop.Order.Contract
{
    public record OrderItemResponse(
        Guid Id,
        Guid OrderId,
        Guid GameId,
        int Quantity,
        decimal PurchasedPrice,
        List<string> Key
    );
}