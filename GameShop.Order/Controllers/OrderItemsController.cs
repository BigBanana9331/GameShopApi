using GameShop.Common;
using GameShop.Order.Entities;
using GamesShop.Contract.Order;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Order.Controllers
{
    class OrderItemsController : ControllerBase
    {
        private readonly IRepository<OrderSumary> _ordersRepository;
        private readonly IRepository<OrderItem> _itemsRepository;
        private readonly IRepository<CatalogItem> _gamesRepository;
        public OrderItemsController(
            IRepository<OrderSumary> ordersRepository,
            IRepository<OrderItem> itemsRepository,
            IRepository<CatalogItem> gamesRepository
        )
        {
            _ordersRepository = ordersRepository;
            _itemsRepository = itemsRepository;
            _gamesRepository = gamesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetAllAsync(Guid orderId)
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
            var orderitems = (await _itemsRepository.GetAllAsync(item => item.OrderId == orderId)).Select(item => MapOrderItemResponse(item));
            return Ok(orderitems);
        }

        private OrderItemResponse MapOrderItemResponse(OrderItem item)
        {
            return new OrderItemResponse(item.Id, item.OrderId,item.GameId, item.Quantity, item.PurchasedPrice);
        }

        
        [HttpPost]
        public async Task<ActionResult<OrderItemResponse>> CreateAsync(OrderItemRequest request)
        {
            var order = await _ordersRepository.GetAsync(request.OrderId);
            if (order == null)
            {
                return NotFound("Order not found!");
            }
            var orderItem = MapOrderItemRequest(request);
            return Ok();
        }

        private OrderItemResponse MapOrderItemRequest(OrderItemRequest request)
        {
            return new OrderItemResponse(Guid.NewGuid(),request.OrderId, request.GameId, request.Quantity, request.PurchasedPrice);
        }

        private static OrderResponse MapOrderResponse(OrderSumary order)
        {
            return new OrderResponse(order.Id, order.UserId, order.Discounted, order.PurchaseDate);
        }
    }
    
}
