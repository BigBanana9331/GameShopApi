using GameShop.Cart.Entities;
using GameShop.Common;
using Microsoft.AspNetCore.Mvc;
using GameShop.Contract.Cart;
namespace GameShop.Cart.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly IRepository<CartItem> _itemsRepository;
        private readonly IRepository<CatalogItem> _gamesRepository;
        private readonly IRepository<Customer> _customerRepository;
        public CartsController(
            IRepository<CartItem> itemsRepository,
            IRepository<CatalogItem> gamesRepository,
            IRepository<Customer> customerRepository
        )
        {
            _itemsRepository = itemsRepository;
            _gamesRepository = gamesRepository;
            _customerRepository = customerRepository;
        }
        // carts/user/id
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<CartItemResponse>>> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var customerEntities = await _customerRepository.GetAsync(id);
            if (customerEntities == null)
            {
                return NotFound(customerEntities);
            }
            var cartItemEntities = await _itemsRepository.GetAllAsync(item => item.UserId == id);
            var gameIds = cartItemEntities.Select(game => game.GameId);
            var gamesEntities = await _gamesRepository.GetAllAsync(game => gameIds.Contains(game.Id));
            var cartItemsResponse = cartItemEntities.Select(cartItem =>
            {
                var game = gamesEntities.Single(game => game.Id == cartItem.GameId);
                return CartItem.MapCartResponse(cartItem, game.Name, game.ImagePath, game.BasePrice, game.CurrentPrice);
            });
            return Ok(cartItemsResponse);
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync(CartItemRequest request)
        {

            var cartItem = await _itemsRepository.GetAsync(item => item.UserId == request.UserId && item.GameId == request.GameId);
            if (cartItem == null)
            {
                var customerEntities = await _customerRepository.GetAsync(request.UserId);
                if (customerEntities == null)
                {
                    return NotFound(customerEntities);
                }
                var gamesEntities = await _gamesRepository.GetAsync(request.GameId);
                if (gamesEntities == null)
                {
                    return NotFound(gamesEntities);
                }
                cartItem = new CartItem(Guid.NewGuid(), request.UserId, request.GameId, request.Quantity);
                await _itemsRepository.CreateAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += request.Quantity;
                await _itemsRepository.UpdateAsync(cartItem);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeteleAsync(Guid id)
        {
            var existingItem = await _itemsRepository.GetAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }
            await _itemsRepository.RemoveAsync(id);
            return NoContent();
        }
        
    }


}