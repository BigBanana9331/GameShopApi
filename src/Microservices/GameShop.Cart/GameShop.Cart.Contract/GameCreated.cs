namespace GameShop.Cart.Contract
{
    public record GameCreated(
        Guid Id,
        string Name,
        string ImagePath,
        decimal BasePrice,
        decimal CurrentPrice
    );
}
