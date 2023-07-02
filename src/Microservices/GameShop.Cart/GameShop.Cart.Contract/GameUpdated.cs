namespace GameShop.Cart.Contract
{
    public record GameUpdated(
        Guid Id,
        string Name,
        string ImagePath,
        decimal BasePrice,
        decimal CurrentPrice
    );
}