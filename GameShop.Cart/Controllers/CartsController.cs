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
        [HttpGet("user/{userId}/items")]
        public async Task<ActionResult<IEnumerable<CartItemResponse>>> GetAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest();
            }
            var customerEntities = await _customerRepository.GetAsync(userId);
            if (customerEntities == null)
            {
                return NotFound(customerEntities);
            }
            var cartItemEntities = await _itemsRepository.GetAllAsync(item => item.UserId == userId);
            var gameIds = cartItemEntities.Select(game => game.GameId);
            var gamesEntities = await _gamesRepository.GetAllAsync(game => gameIds.Contains(game.Id));
            var cartItemsResponse = cartItemEntities.Select(cartItem =>
            {
                var game = gamesEntities.Single(game => game.Id == cartItem.GameId);
                return CartItem.MapCartResponse(cartItem, game.Name, game.ImagePath, game.BasePrice, game.CurrentPrice);
            });
            return Ok(cartItemsResponse);
        }
        [HttpPost("user/{userId}/items")]
        public async Task<ActionResult> CreateAsync(Guid userId, CartItemRequest request)
        {

            var cartItem = await _itemsRepository.GetAsync(item => item.UserId == userId && item.GameId == request.GameId);
            if (cartItem == null)
            {
                var customerEntities = await _customerRepository.GetAsync(userId);
                if (customerEntities == null)
                {
                    return NotFound(customerEntities);
                }
                var gamesEntities = await _gamesRepository.GetAsync(request.GameId);
                if (gamesEntities == null)
                {
                    return NotFound(gamesEntities);
                }
                cartItem = new CartItem(Guid.NewGuid(), userId, request.GameId, request.Quantity);
                await _itemsRepository.CreateAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += request.Quantity;
                await _itemsRepository.UpdateAsync(cartItem);
            }
            return Ok();
        }
        [HttpDelete("user/{userId}/items/{cartItemId}")]
        public async Task<ActionResult> DeteleAsync(Guid cartItemId)
        {
            var existingItem = await _itemsRepository.GetAsync(cartItemId);
            if (existingItem == null)
            {
                return NotFound();
            }
            await _itemsRepository.RemoveAsync(cartItemId);
            return NoContent();
        }

    }
}





























// using GameShop.Cart.Entities;
// using GameShop.Common;
// using Microsoft.AspNetCore.Mvc;
// using GameShop.Contract.Cart;
// namespace GameShop.Cart.Controller
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class CartsController : ControllerBase
//     {
//         private readonly IRepository<CustomerCart> _cartRepository;
//         private readonly IRepository<CatalogItem> _gamesRepository;
//         private readonly IRepository<Customer> _customerRepository;
//         public CartsController(
//             IRepository<CustomerCart> cartRepository,
//             IRepository<CatalogItem> gamesRepository,
//             IRepository<Customer> customerRepository
//         )
//         {
//             _cartRepository = cartRepository;
//             _gamesRepository = gamesRepository;
//             _customerRepository = customerRepository;
//         }

//         [HttpPost("{userId}")]
//         public async Task<ActionResult> CreateAsync(Guid userId, CartItemRequest request)
//         {
//             if (userId == Guid.Empty)
//             {
//                 return BadRequest("User not found!");
//             }
//             var customerEntities = await _customerRepository.GetAsync(userId);
//             if (customerEntities == null)
//             {
//                 return NotFound(customerEntities);
//             }
//             // ... define after getting the List/Enumerable/whatever
//             // var dict = myList.ToDictionary(x => x.MyProperty);
//             // ... somewhere in code
//             // MyObject found;
//             // if (dict.TryGetValue(myValue, out found)) found.OtherProperty = newValue;
//             var cart = await _cartRepository.GetAsync(userId);
//             var game = await _gamesRepository.GetAsync(request.GameId);
//             if (game == null)
//             {
//                 return BadRequest("Game not found!");
//             }
//             if (cart != null)
//             {
//                 if (cart.Items.FirstOrDefault(i => i.Id == request.GameId) != null)
//                 {
//                     // var index = cart.Items.IndexOf(existedItem);
//                     // existedItem.Quantity += request.Quantity;
//                     // cart.Items.RemoveAt(index);
//                     // cart.Items.Insert(index, existedItem);
//                     cart.Items.FirstOrDefault(i => i.Id == request.GameId).Quantity += request.Quantity;
//                     await _cartRepository.UpdateAsync(cart);
//                     return NoContent();
//                 }
//                 else
//                 {
//                     var item = new CartItem(
//                                     request.GameId,
//                                     game.Name,
//                                     game.ImagePath,
//                                     game.BasePrice,
//                                     game.CurrentPrice,
//                                     request.Quantity);
//                     cart.Items.Append(item);
//                     return NoContent();
//                 }
//             }
//             else
//             {
//                 var item = new CartItem(
//                                     request.GameId,
//                                     game.Name,
//                                     game.ImagePath,
//                                     game.BasePrice,
//                                     game.CurrentPrice,
//                                     request.Quantity);
//                 cart = new CustomerCart(Guid.NewGuid(), new List<CartItem> { item });
//                 await _cartRepository.CreateAsync(cart);
//                 return NoContent();
//             }
//         }
//         // carts/userId
//         [HttpGet("{userId}")]
//         public async Task<ActionResult<CustomerCart>> GetAsync(Guid userId)
//         {
//             if (userId == Guid.Empty)
//             {
//                 return BadRequest();
//             }
//             var customerEntities = await _customerRepository.GetAsync(userId);
//             if (customerEntities == null)
//             {
//                 return NotFound(customerEntities);
//             }
//             var cart = await _cartRepository.GetAsync(userId);
//             return Ok(cart);
//         }

//         [HttpDelete("{userId}/items/{cartItemId}")]
//         public async Task<ActionResult> DeteleAsync(Guid userId, Guid cartItemId)
//         {
//             var cart = await _cartRepository.GetAsync(userId);
//             if (cart == null){
//                 return BadRequest();
//             }
//             var index  = cart.Items.FindIndex(i => i.Id == cartItemId);
//             cart.Items.RemoveAt(index);
//             await _cartRepository.UpdateAsync(cart);
//             return NoContent();
//             // await _cartRepository.RemoveAsync(cartItemId);
//             // return NoContent();
//         }

//     }
// }