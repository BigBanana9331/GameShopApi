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
        public async Task<ActionResult> CreateAsync(OrderRequest request)
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
        public async Task<IEnumerable<OrderResponse>> GetAllAsync()
        {
            var orders = (await _ordersRepository.GetAllAsync()).Select(order =>OrderSumary.MapOrderResponse(order));
            return orders;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var order = await _ordersRepository.GetAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return OrderSumary.MapOrderResponse(order);
        }

        [HttpGet("user/{id}")]
        public async Task<IEnumerable<OrderResponse>> GetAllByUserAsync(Guid id)
        {
            var orders = (await _ordersRepository.GetAllAsync(order => order.UserId == id))
                                    .Select(order =>OrderSumary.MapOrderResponse(order));
            return orders;
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, OrderRequest request)
        {
            var order = await _ordersRepository.GetAsync(id);
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var order = await _ordersRepository.GetAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            await _ordersRepository.RemoveAsync(id);
            return NoContent();
        }
        
        // OrderItem action
        [HttpGet("{id}/items")]
        public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetAllAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var order = await _ordersRepository.GetAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderitems = (await _itemsRepository.GetAllAsync(item => item.OrderId == id)).Select(item => OrderItem.MapOrderItemResponse(item));
            return Ok(orderitems);
        }
        [HttpDelete("items/{id}")]
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
        
        [HttpPost]
        public async Task<ActionResult<OrderItemResponse>> CreateAsync(OrderItemRequest request)
        {
            var order = await _ordersRepository.GetAsync(request.OrderId);
            if (order == null)
            {
                return NotFound("Order not found!");
            }
            var orderItem = OrderItem.MapOrderItemRequest(request);
            await _itemsRepository.CreateAsync(orderItem);
            return Ok();
        }
    }
}