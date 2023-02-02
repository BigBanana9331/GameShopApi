using GameShop.Common;
using GameShop.Contract.Cart;

namespace GameShop.Cart.Entities
{
    public class CartItem : IEntity
    {
        public Guid Id { get; set;}
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public int Quantity { get; set; }

        public CartItem(Guid id, Guid userId, Guid gameId, int quantity)
        {
            Id = id;
            UserId = userId;
            GameId = gameId;
            Quantity = quantity;
        }
        public static CartItemResponse MapCartResponse(
            CartItem item,
            string name,
            string imagePath,
            decimal basePrice,
            decimal currentPrice
            )
        {
            return new CartItemResponse(
                item.Id,
                item.UserId,
                item.GameId,
                name,
                imagePath,
                basePrice,
                currentPrice,
                item.Quantity
            );
        }
    }

}














// using GameShop.Common;

// namespace GameShop.Cart.Entities
// {
//     public class CartItem : IEntity
//     {
//         public Guid Id { get; }
//         public Guid UserId { get; }
//         public Guid GameId { get; }
//         public int Quantity { get; }

//         private CartItem(Guid id, Guid userId, Guid gameId, int quantity)
//         {
//             Id = id;
//             UserId = userId;
//             GameId = gameId;
//             Quantity = quantity;
//         }
//         public static CartItem Create(Guid userId, Guid gameId, int quantity, Guid? id = null)
//         {
//             return new CartItem(id ?? Guid.NewGuid(), userId, gameId, quantity);
//         }
//     }

// }