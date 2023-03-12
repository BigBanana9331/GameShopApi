using GameShop.Common;
using GameShop.Contract.Cart;

namespace GameShop.Cart.Entities
{
    public class CartItem : IEntity
    {
        public Guid Id { get; set; }
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
        // public static CartItem Create(Guid userId, Guid gameId, int quantity, Guid? id = null)
        // {
        //     return new CartItem(id ?? Guid.NewGuid(), userId, gameId, quantity);
        // }
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
// using GameShop.Contract.Cart;

// namespace GameShop.Cart.Entities
// {
//     public class CartItem : IEntity
//     {
//         public Guid Id { get; set; } //gameId
//         public string Name { get; set; }
//         public string ImagePath { get; set; }
//         public Decimal BasePrice { get; set; }
//         public Decimal CurrentPrice { get; set; }
//         public int Quantity { get; set; }

//         public CartItem(
//             Guid id,
//             string name,
//             string imagePath,
//             decimal basePrice,
//             decimal currentPrice,
//             int quantity)
//         {
//             Id = id;
//             Name = name;
//             ImagePath = imagePath;
//             BasePrice = basePrice;
//             CurrentPrice = currentPrice;
//         }
//         // public static CartItem MapItemRequest(CartItemRequest request)
//         // {
//         //     return new CartItem(Guid.NewGuid(), request.GameId, request.)
//         // }
//         // public static CartItemResponse MapCartResponse(
//         //     CartItem item,
//         //     string name,
//         //     string imagePath,
//         //     decimal basePrice,
//         //     decimal currentPrice
//         //     )
//         // {
//         //     return new CartItemResponse(
//         //         item.Id,
//         //         item.UserId,
//         //         item.GameId,
//         //         name,
//         //         imagePath,
//         //         basePrice,
//         //         currentPrice,
//         //         item.Quantity
//         //     );
//         // }
//     }

// }