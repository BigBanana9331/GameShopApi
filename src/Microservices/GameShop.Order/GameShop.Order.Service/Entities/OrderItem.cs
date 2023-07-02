using System.Security.Cryptography;
using GameShop.Common;
using GamesShop.Order.Contract;

namespace GameShop.Order.Entities
{
    public class OrderItem : IEntity
    {
        public Guid Id { get; }
        public Guid OrderId { get; set; }
        public Guid GameId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasedPrice { get; set; }
        public List<string> KeyCode { get; set; }
        public OrderItem(Guid id, Guid orderId, Guid gameId, int quantity, decimal purchasedPrice, List<string> keyCode)
        {
            Id = id;
            OrderId = orderId;
            GameId = gameId;
            Quantity = quantity;
            PurchasedPrice = purchasedPrice;
            KeyCode = keyCode;
        }
        public static OrderItemResponse MapOrderItemResponse(OrderItem item)
        {
            return new OrderItemResponse(item.Id, item.OrderId, item.GameId, item.Quantity, item.PurchasedPrice, item.KeyCode);
        }
        public static OrderItem MapOrderItemRequest(OrderItemRequest request)
        {
            var key = GenerateKeyCode();
            var keys = new List<string> { key };
            return new OrderItem(Guid.NewGuid(), request.OrderId, request.GameId, request.Quantity, request.PurchasedPrice, keys);
        }

        private static string GenerateKeyCode()
        {
            var random = new byte[24];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
    }
}



// using GameShop.Common;
// using GamesShop.Contract.Order;

// namespace GameShop.Order.Entities{
//     public class OrderItem : IEntity
//     {
//         public Guid Id { get; } // GameID
//         public int Quantity { get; set; }
//         public decimal PurchasedPrice { get; set; }
//         public OrderItem(Guid id, Guid orderId, Guid gameId, int quantity, decimal purchasedPrice)
//         {
//             Id = id;
//             Quantity = quantity;
//             PurchasedPrice = purchasedPrice;
//         }
//         // public static OrderItemResponse MapOrderItemResponse(OrderItem item)
//         // {
//         //     return new OrderItemResponse(item.Id, item.OrderId,item.GameId, item.Quantity, item.PurchasedPrice);
//         // }
//         // public static OrderItem MapOrderItemRequest(OrderItemRequest request)
//         // {
//         //     return new OrderItem(Guid.NewGuid(),request.OrderId, request.GameId, request.Quantity, request.PurchasedPrice);
//         // }
//     }
// }
