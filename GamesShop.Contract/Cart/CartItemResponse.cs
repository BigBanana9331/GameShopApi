namespace GamesShop.Contract.Cart
{
    public record CartItemResponse(
        Guid Id,
        Guid UserId,
        Guid GameId,
        int Quantity
    );
}
