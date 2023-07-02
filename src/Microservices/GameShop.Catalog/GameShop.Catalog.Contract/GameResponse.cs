namespace GameShop.Catalog.Contract
{
    public record GameResponse(
        Guid Id,
        string Name,
        string ImagePath,
        string Platform,
        DateTime DateRelease,
        decimal BasePrice,
        decimal CurrentPrice,
        List<string> Genre,
        Dictionary<string, string> SystemRequirement,
        List<string> Assets,
        double Rating,
        string Publisher,
        string Developer
    );
}

