namespace GameShop.Catalog.Contract{
    public record GameCreated(
        Guid GameId,
        string Name,
        string ImagePath,
        string Platform,
        DateTime DateRelease,
        decimal BasePrice,
        decimal CurrentPrice,
        List<string> Genre,
        double Rating,
        string Publisher,
        string Developer
    );

    public record GameUpdated(
        Guid GameId,
        string Name,
        string ImagePath,
        string Platform,
        DateTime DateRelease,
        decimal BasePrice,
        decimal CurrentPrice,
        List<string> Genre,
        double Rating,
        string Publisher,
        string Developer
    );
    public record GameDeleted(
        Guid GameId
    );
}