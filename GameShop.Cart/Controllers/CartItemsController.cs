using GameShop.Cart.Client;
using GameShop.Cart.Dtos;
using GameShop.Cart.Entities;
using GameShop.Common;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Cart.Controller
{
    [ApiController]
    [Route("cart")]
    public class CartItemsController : ControllerBase
    {
        public readonly IRepository<CartItem> _itemsRepository;
        public readonly GameClient _gameClient;

        public CartItemsController(IRepository<CartItem> itemsRepository, GameClient gameClient)
        {
            _itemsRepository = itemsRepository;
            _gameClient = gameClient;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemReponse>>> GetAsync(Guid UserId)
        {
            if (UserId == Guid.Empty)
            {
                return BadRequest();
            }

            // var items = (await _itemsRepository.GetAllAsync(item => item.UserId == UserId)).Select(item => item.MapCartResponse());
            var catalog = await _gameClient.GetCartItemsAsync();
            var cartItemEntities = await _itemsRepository.GetAllAsync(item => item.UserId == UserId);
            var cartItemsResponse = cartItemEntities.Select(cartItem=>{
                var game = catalog.Single(game => game.GameId == cartItem.GameId);
                return cartItem.MapCartResponse(game.Name, game.ImagePath, game.Platform, game.DateRelease, game.BasePrice, game.CurrentPrice, game.Genre, game.Rating, game.Publisher, game.Developer);
            });
            return Ok(cartItemsResponse);
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync(GrantCartRequest request)
        {
            var cartItem = await _itemsRepository.GetAsync(item => item.UserId == request.UserId && item.GameId == request.GamesId);
            if (cartItem == null)
            {
                cartItem = CartItem.Create(request.UserId, request.GamesId, request.Quantity);
                await _itemsRepository.CreateAsync(cartItem);
            }
            else
            {
                cartItem = CartItem.Create(request.UserId, request.GamesId, request.Quantity, cartItem.Id);
                await _itemsRepository.UpdateAsync(cartItem);
            }
            return Ok();
        }
    }
}