using GamesShop.Models;

namespace GamesShop.Dtos
{
    public static class Extensions
    {
        public static GameDto AsDto(this Game game)
        {
            return new GameDto(
                        game.Id,
                        game.Name,
                        game.ImagePath,
                        game.Platform,
                        game.DateRelease,
                        game.BasePrice,
                        game.CurrentPrice,
                        game.Genre,
                        game.Rating,
                        game.Publisher,
                        game.Developer
                    );
        }
    }
}