using Microsoft.AspNetCore.Mvc;
using GamesShop.Contract;
using GamesShop.Entities;
using GamesShop.Common;

namespace GamesShop.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : ControllerBase
    {
        private readonly IRepository<Game>? _gameRepository;

        public GamesController(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpPost()]
        public async Task<ActionResult<Game>> CreateGameAsync(GameRequest request)
        {
            var game = MapGameRequest(request);
            await _gameRepository.CreateAsync(game);
            return CreatedAtAction(
                nameof(GetGameAsync),
                new { id = game.Id },
                game
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GameResponse>> GetGameAsync(Guid id)
        {
            var game = await _gameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            var response = MapGameResponse(game);
            return response;
        }

        [HttpGet()]
        public async Task<IEnumerable<GameResponse>> GetAllAsync()
        {
            var games = (await _gameRepository.GetAllAsync()).Select(game => MapGameResponse(game));
            return games;
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateGameAsync(Guid id, GameRequest request)
        {
            var existingGame = await _gameRepository.GetAsync(id);
            if (existingGame == null)
            {
                return NotFound();
            }
            var game = MapGameRequest(request, id);
            await _gameRepository.UpdateAsync(game);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGameAsync(Guid id)
        {
            var existingGame = await _gameRepository.GetAsync(id);
            if (existingGame == null)
            {
                return NotFound();
            }
            await _gameRepository.RemoveAsync(id);
            return NoContent();
        }

        private static Game MapGameRequest(GameRequest request, Guid? id = null)
        {
            return Game.Create(
                        request.Name,
                        request.ImagePath,
                        request.Platform,
                        request.DateRelease,
                        request.BasePrice,
                        request.CurrentPrice,
                        request.Genre,
                        request.Rating,
                        request.Publisher,
                        request.Developer,
                        id
                    );
        }
        private static GameResponse MapGameResponse(Game game)
        {
            return new GameResponse(
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
