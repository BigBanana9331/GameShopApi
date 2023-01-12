namespace GameShop.Contract.Cart
{
    public record CartItemResponse(
        Guid Id,
        Guid UserId,
        Guid GameId,
        string Name,
        string ImagePath,
        decimal BasePrice,
        decimal CurrentPrice,
        int Quantity
    );
}
