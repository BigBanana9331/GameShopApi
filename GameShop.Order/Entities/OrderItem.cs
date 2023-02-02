using GameShop.Common;
using GamesShop.Contract.Order;

namespace GameShop.Order.Entities{
    public class OrderItem : IEntity
    {
        public Guid Id { get; }
        public Guid OrderId { get; set; }
        public Guid GameId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasedPrice { get; set; }
        public OrderItem(Guid id, Guid orderId, Guid gameId, int quantity, decimal purchasedPrice)
        {
            Id = id;
            OrderId = orderId;
            GameId = gameId;
            Quantity = quantity;
            PurchasedPrice = purchasedPrice;
        }
        public static OrderItemResponse MapOrderItemResponse(OrderItem item)
        {
            return new OrderItemResponse(item.Id, item.OrderId,item.GameId, item.Quantity, item.PurchasedPrice);
        }
        public static OrderItem MapOrderItemRequest(OrderItemRequest request)
        {
            return new OrderItem(Guid.NewGuid(),request.OrderId, request.GameId, request.Quantity, request.PurchasedPrice);
        }
    }
}