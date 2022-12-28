namespace GamesShop.Dtos
{
    public record GameDto(
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
    // DateTime CreateDate,
    // DateTime LastModified
    );
    public record CreateGameDto(
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

    public record UpdateGameDto(
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