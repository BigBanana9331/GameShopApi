namespace GameShop.Catalog.Contract
{
    public record GameCreated(
        Guid Id,
        string Name,
        string ImagePath,
        decimal BasePrice,
        decimal CurrentPrice
    );
}
