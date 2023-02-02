using GameShop.Common;
using GamesShop.Contract.Order;

namespace GameShop.Order.Entities
{
    public class OrderSumary : IEntity
    {
        public Guid Id { get; }
        public Guid UserId { get; set; }
        public decimal Discounted { get; set; }
        public DateOnly PurchaseDate { get; set; }
        private OrderSumary(Guid id, Guid userId, decimal discounted, DateOnly purchaseDate)
        {
            Id = id;
            UserId = userId;
            Discounted = discounted;
            PurchaseDate = purchaseDate;
        }

        public static OrderResponse MapOrderResponse(OrderSumary order)
        {
            return new OrderResponse(order.Id, order.UserId, order.Discounted, order.PurchaseDate);
        }
        public static OrderSumary MapOrderRequest(OrderRequest request)
        {
            return new OrderSumary(Guid.NewGuid(), request.UserId, request.Discounted, request.PurchasedDate);
        }

        // public OrderSumary Create(Guid userId, decimal discounted, DateOnly purchaseDate)
        // {
        //     return new OrderSumary(Guid.NewGuid(), userId, discounted, purchaseDate);
        // }
    }
}