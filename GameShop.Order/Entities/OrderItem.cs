using GameShop.Common;

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
    }
}