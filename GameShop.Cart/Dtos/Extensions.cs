// using GameShop.Cart.Entities;
// using GameShop.Contract.Cart;
// namespace GameShop.Cart.Dtos
// {
//     public static class Extension
//     {
//         public static CartItemResponse MapCartResponse(
//             this CartItem item,
//             string name,
//             string imagePath,
//             string platform,
//             DateTime dateRelease,
//             decimal basePrice,
//             decimal currentPrice,
//             List<string> genre,
//             double rating,
//             string publisher,
//             string developer
//             )
//         {
//             return new CartItemResponse(
//                 item.Id,
//                 item.UserId,
//                 item.GameId,
//                 name,
//                 imagePath,
//                 platform,
//                 dateRelease,
//                 basePrice,
//                 currentPrice,
//                 genre,
//                 rating,
//                 publisher,
//                 developer,
//                 item.Quantity
//             );
//         }
//     }
// }