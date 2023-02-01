using GameShop.Common;

namespace GameShop.Order.Entities
{
    public class OrderSumary : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Discounted { get; set; }
        public DateTime PurchaseDate { get; set; }
        public OrderSumary(Guid id, Guid userId, decimal discounted, DateTime purchaseDate)
        {
            Id = id;
            UserId = userId;
            Discounted = discounted;
            PurchaseDate = purchaseDate;
        }
        // public Order Create(Guid userId, decimal discounted, DateTime purchaseDate)
        // {
        //     return new Order(Guid.NewGuid(), userId, discounted, purchaseDate);
        // }
    }
}