using Microsoft.AspNetCore.Mvc;
using GameShop.Catalog.Contract;
using GameShop.Catalog.Entities;
using Microsoft.AspNetCore.Authorization;
using GameShop.Common;

namespace GameShop.Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IRepository<Game> _gameRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public GamesController(IRepository<Game> gameRepository, IPublishEndpoint publishEndpoint)
        {
            _gameRepository = gameRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Game>> CreateGameAsync(GameRequest request)
        {
            var game = Game.MapGameRequest(request);
            Console.WriteLine(game.Id);
            await _gameRepository.CreateAsync(game);
            await _publishEndpoint.Publish(new GameCreated(
                game.Id,
                game.Name,
                game.ImagePath,
                game.BasePrice,
                game.CurrentPrice
            ));
            return CreatedAtAction(
                nameof(GetGameByIdAsync),
                new { gameId = game.Id },
                game
            );
        }

        [HttpGet]
        public async Task<IEnumerable<GameResponse>> GetAllAsync()
        {

            var games = (await _gameRepository.GetAllAsync()).Select(game => Game.MapGameResponse(game));
            return games;

            // var games = await _gameRepository.GetAllAsync();
            // foreach (var item in games)
            // {
            //     Console.WriteLine(item.Id);
            // }
            // return games.Select(game => Game.MapGameResponse(game));
        }

        // games/id
        [HttpGet("{gameId}")]
        public async Task<ActionResult<GameResponse>> GetGameByIdAsync(Guid gameId)
        {
            var game = await _gameRepository.GetAsync(gameId);
            if (game == null)
            {
                return NotFound();
            }
            var response = Game.MapGameResponse(game);
            return response;
        }

        // games/id
        [HttpPut("{gameId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateGameAsync(Guid gameId, GameRequest request)
        {
            var existingGame = await _gameRepository.GetAsync(gameId);
            if (existingGame == null)
            {
                return NotFound();
            }
            var game = Game.MapGameRequest(request, gameId);
            await _gameRepository.UpdateAsync(game);
            await _publishEndpoint.Publish(new GameUpdated(
                game.Id,
                game.Name,
                game.ImagePath,
                game.BasePrice,
                game.CurrentPrice
                ));
            return NoContent();
        }

        // games/id
        [HttpDelete("{gameId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteGameAsync(Guid gameId)
        {
            var existingGame = await _gameRepository.GetAsync(gameId);
            if (existingGame == null)
            {
                return NotFound();
            }
            await _gameRepository.RemoveAsync(gameId);
            await _publishEndpoint.Publish(new GameDeleted(gameId));
            return NoContent();
        }
    }
}
