namespace GameShop.Contract.Game
{
    public record GameRequest(
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
}

