// using GameShop.Common;
// using GameShop.Order.Entities;
// using GamesShop.Contract.Order;
// using Microsoft.AspNetCore.Mvc;

// namespace GameShop.Order.Controllers
// {
//     class OrderItemsController : ControllerBase
//     {
//         private readonly IRepository<OrderSumary> _ordersRepository;
//         private readonly IRepository<OrderItem> _itemsRepository;
//         private readonly IRepository<CatalogItem> _gamesRepository;
        
//         public OrderItemsController(
//             IRepository<OrderSumary> ordersRepository,
//             IRepository<OrderItem> itemsRepository,
//             IRepository<CatalogItem> gamesRepository
//         )
//         {
//             _ordersRepository = ordersRepository;
//             _itemsRepository = itemsRepository;
//             _gamesRepository = gamesRepository;
//         }

        
//         // private OrderItemResponse MapOrderItemResponse(OrderItem item)
//         // {
//         //     return new OrderItemResponse(item.Id, item.OrderId,item.GameId, item.Quantity, item.PurchasedPrice);
//         // }
//         // private OrderItem MapOrderItemRequest(OrderItemRequest request)
//         // {
//         //     return new OrderItemResponse(Guid.NewGuid(),request.OrderId, request.GameId, request.Quantity, request.PurchasedPrice);
//         // }

//     }
    
// }
