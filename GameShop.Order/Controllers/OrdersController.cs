using GameShop.Common;
using Microsoft.AspNetCore.Mvc;
using GameShop.Order.Entities;
using GamesShop.Contract.Order;

namespace GameShop.Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<OrderSumary> _ordersRepository;
        private readonly IRepository<Customer> _customerRepository;
        public OrdersController(IRepository<OrderSumary> ordersRepository, IRepository<Customer> customerRepository)
        {
            _ordersRepository = ordersRepository;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<OrderResponse>> GetAsync(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return BadRequest();
            }
            var order = await _ordersRepository.GetAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return MapOrderResponse(order);
        }

        [HttpGet]
        public async Task<IEnumerable<OrderResponse>> GetAllAsync()
        {
            var orders = (await _ordersRepository.GetAllAsync()).Select(order => MapOrderResponse(order));
            return orders;
        }
        
        [HttpGet]
        // [Route("[controller]/user/id")]
        public async Task<IEnumerable<OrderResponse>> GetAllByUserAsync(Guid UserId)
        {
            var orders = (await _ordersRepository.GetAllAsync(order => order.UserId == UserId))
                                    .Select(order => MapOrderResponse(order));
            return orders;
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync(OrderRequest request)
        {
            var user = await _customerRepository.GetAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found!");
            }
            var order = MapOrderRequest(request);
            return Ok();
            // var cartItem = await _itemsRepository.GetAsync(item => item.UserId == request.UserId && item.GameId == request.GameId);
            // if (cartItem == null)
            // {
            //     var customerEntities = await _customerRepository.GetAsync(request.UserId);
            //     if (customerEntities == null)
            //     {
            //         return NotFound(customerEntities);
            //     }
            //     var gamesEntities = await _gamesRepository.GetAsync(request.GameId);
            //     if (gamesEntities == null)
            //     {
            //         return NotFound(gamesEntities);
            //     }
            //     cartItem = new CartItem(Guid.NewGuid(), request.UserId, request.GameId, request.Quantity);
            //     await _itemsRepository.CreateAsync(cartItem);
            // }
            // else
            // {
            //     cartItem.Quantity += request.Quantity;
            //     await _itemsRepository.UpdateAsync(cartItem);
            // }
            // return Ok();
        }
        [HttpPut("{orderId:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid orderId, OrderRequest request)
        {
            var order = await _ordersRepository.GetAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            order.Discounted = request.Discounted;
            order.PurchaseDate = request.PurchasedDate;
            order.UserId = request.UserId;
            await _ordersRepository.UpdateAsync(order);
            return NoContent();
        }
        [HttpDelete("{orderId:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid orderId)
        {
            var order = await _ordersRepository.GetAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            await _ordersRepository.RemoveAsync(orderId);
            return NoContent();
        }
        private static OrderResponse MapOrderResponse(OrderSumary order)
        {
            return new OrderResponse(order.Id, order.UserId, order.Discounted, order.PurchaseDate);
        }
        private OrderSumary MapOrderRequest(OrderRequest request)
        {
            return new OrderSumary(Guid.NewGuid(), request.UserId, request.Discounted, request.PurchasedDate);
        }
    }
}