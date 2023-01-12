namespace GameShop.Contract.Game
{
    public record GameUpdated(
        Guid Id,
        string Name,
        string ImagePath,
        decimal BasePrice,
        decimal CurrentPrice
    );
}