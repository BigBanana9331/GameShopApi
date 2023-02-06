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
        private readonly IRepository<OrderItem> _itemsRepository;
        private readonly IRepository<CatalogItem> _gamesRepository;



        public OrdersController(
            IRepository<OrderSumary> ordersRepository,
            IRepository<Customer> customerRepository,
            IRepository<OrderItem> itemsRepository,
            IRepository<CatalogItem> gamesRepository
        )
        {
            _ordersRepository = ordersRepository;
            _customerRepository = customerRepository;
            _itemsRepository = itemsRepository;
            _gamesRepository = gamesRepository;
        }


        [HttpPost]
        public async Task<ActionResult> CreateOrderAsync(OrderRequest request)
        {
            var user = await _customerRepository.GetAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found!");
            }
            var order = OrderSumary.MapOrderRequest(request);
            await _ordersRepository.CreateAsync(order);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<OrderResponse>> GetAllOrdersAsync()
        {
            var orders = (await _ordersRepository.GetAllAsync()).Select(order => OrderSumary.MapOrderResponse(order));
            return orders;
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResponse>> GetOrderByIdAsync(Guid orderId)
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
            return OrderSumary.MapOrderResponse(order);
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult> UpdateOrderAsync(Guid orderId, OrderRequest request)
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

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrderAsync(Guid orderId)
        {
            var order = await _ordersRepository.GetAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            await _ordersRepository.RemoveAsync(orderId);
            return NoContent();
        }

        [HttpGet("user/{userId}")]  // orders/user/{userId}
        public async Task<IEnumerable<OrderResponse>> GetAllOrderItemsByUserAsync(Guid userId)
        {
            var orders = (await _ordersRepository.GetAllAsync(order => order.UserId == userId))
                                    .Select(order => OrderSumary.MapOrderResponse(order));
            return orders;
        }


        // OrderItem action
        [HttpGet("{orderId}/items")]
        public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetAllOrderItemsAsync(Guid orderId)
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
            var orderitems = (await _itemsRepository.GetAllAsync(item => item.OrderId == orderId)).Select(item => OrderItem.MapOrderItemResponse(item));
            return Ok(orderitems);
        }
        [HttpPost("{orderId}/items")]
        public async Task<ActionResult<OrderItemResponse>> CreateOrderItemAsync(Guid orderId, OrderItemRequest request)
        {
            var order = await _ordersRepository.GetAsync(orderId);
            if (order == null)
            {
                return NotFound("Order not found!");
            }
            var orderItem = OrderItem.MapOrderItemRequest(request);
            await _itemsRepository.CreateAsync(orderItem);
            return Ok();
        }
        [HttpDelete("{orderId}/items/{orderItemId}")]
        public async Task<ActionResult> DeteleOrderItemAsync(Guid orderItemId)
        {
            var existingItem = await _itemsRepository.GetAsync(orderItemId);
            if (existingItem == null)
            {
                return NotFound();
            }
            await _itemsRepository.RemoveAsync(orderItemId);
            return NoContent();
        }
    }
}