// using GameShop.Catalog.Entities;
// using ErrorOr;
// using GameShop.Catalog.ServiceErrors;

// namespace GameShop.Catalog.Services;
// public class GameService : IGameService
// {
//     private static readonly Dictionary<Guid, Game> _game = new();
//     public ErrorOr<Created> CreateGame(Game game)
//     {
//         _game.Add(game.Id, game);
//         return Result.Created;
//     }

//     public ErrorOr<Game> GetGame(Guid id)
//     {
//         if (_game.TryGetValue(id, out var game)){
//             return game;
//         }
//         return Errors.Game.NotFound;
//     }
//     public ErrorOr<UpdatedGame> UpdateGame(Game game)
//     {
//         var isNewlyCreated = ! _game.ContainsKey(game.Id);
//         _game[game.Id] = game;
//         return new UpdatedGame(isNewlyCreated);
//     }
//     public ErrorOr<Deleted> DeleteGame(Guid id)
//     {
//         _game.Remove(id);
//         return Result.Deleted;
//     }
// }