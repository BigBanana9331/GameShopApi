namespace GameShop.Contract.Cart
{
    public record CartItemRequest(
        Guid UserId,
        Guid GameId,
        int Quantity
    );
}
