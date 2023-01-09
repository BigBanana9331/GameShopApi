using GameShop.Common;

namespace GameShop.Cart.Entities
{
    public class CartItem : IEntity
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public Guid GameId { get; }
        public int Quantity { get; }
        public decimal total { get; }

        private CartItem(Guid id, Guid userId, Guid gameId, int quantity)
        {
            Id = id;
            UserId = userId;
            GameId = gameId;
            Quantity = quantity;
        }
        public static CartItem Create(Guid userId, Guid gameId, int quantity, Guid? id = null)
        {
            return new CartItem(id ?? Guid.NewGuid(),userId, gameId, quantity);
        }
    }

}