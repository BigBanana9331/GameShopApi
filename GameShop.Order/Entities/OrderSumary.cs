using GameShop.Common;

namespace GameShop.Order.Entities
{
    public class OrderSumary : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Discounted { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public OrderSumary(Guid id, Guid userId, decimal discounted, DateOnly purchaseDate)
        {
            Id = id;
            UserId = userId;
            Discounted = discounted;
            PurchaseDate = purchaseDate;
        }
        // public Order Create(Guid userId, decimal discounted, DateOnly purchaseDate)
        // {
        //     return new Order(Guid.NewGuid(), userId, discounted, purchaseDate);
        // }
    }
}