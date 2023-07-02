namespace GameShop.Catalog.Contract
{
    public record GameUpdated(
        Guid Id,
        string Name,
        string ImagePath,
        decimal BasePrice,
        decimal CurrentPrice
    );
}