namespace GamesShop.Contract.Order
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