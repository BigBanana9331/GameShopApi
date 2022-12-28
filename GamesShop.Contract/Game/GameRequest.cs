namespace GamesShop.Contract
{
    public record GameRequest(
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

