namespace GameShop.Contract.Game
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
        double Rating,
        string Publisher,
        string Developer
    );
}
