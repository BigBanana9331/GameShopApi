using GamesShop.Cart.Entities;
using GamesShop.Common;
using Microsoft.AspNetCore.Mvc;

namespace GamesShop.Cart.Controller
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        public readonly IRepository<CartItem> _itemsRepository;

        public ItemsController(IRepository<CartItem> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemReponse>>> GetAsync(Guid UserId)
        {
            if (UserId == Guid.Empty)
            {
                return BadRequest();
            }

            var items = (await _itemsRepository.GetAllAsync(item => item.UserId == UserId)).Select(item => item.MapCartResponse());
            return Ok(items);
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