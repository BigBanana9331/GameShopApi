using Microsoft.AspNetCore.Mvc;
using GameShop.Contract.Game;
using GameShop.Catalog.Entities;
using GameShop.Common;
using MassTransit;

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
        public async Task<ActionResult<Game>> CreateGameAsync(GameRequest request)
        {
            var game = Game.MapGameRequest(request);
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
                new { id = game.Id },
                game
            );
        }

        [HttpGet]
        public async Task<IEnumerable<GameResponse>> GetAllAsync()
        {

            var games = (await _gameRepository.GetAllAsync()).Select(game => Game.MapGameResponse(game));
            return games;
        }

        // games/id
        [HttpGet("{id}")]
        public async Task<ActionResult<GameResponse>> GetGameByIdAsync(Guid id)
        {
            var game = await _gameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            var response = Game.MapGameResponse(game);
            return response;
        }

        // games/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameAsync(Guid id, GameRequest request)
        {
            var existingGame = await _gameRepository.GetAsync(id);
            if (existingGame == null)
            {
                return NotFound();
            }
            var game = Game.MapGameRequest(request, id);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameAsync(Guid id)
        {
            var existingGame = await _gameRepository.GetAsync(id);
            if (existingGame == null)
            {
                return NotFound();
            }
            await _gameRepository.RemoveAsync(id);
            await _publishEndpoint.Publish(new GameDeleted(id));
            return NoContent();
        }
    }
}
