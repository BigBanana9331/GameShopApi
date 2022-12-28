namespace GamesShop.Contract
{
    public record GameResponse(
        Guid id,
        string Name,
        string ImagePath,
        string Platform,
        DateTime DateRelease,
        Decimal BasePrice,
        Decimal CurrentPrice,
        List<string> Genre,
        double Rating,
        string Publisher,
        string Developer
    );
}

