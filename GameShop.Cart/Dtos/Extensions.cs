using GameShop.Cart.Entities;

namespace GameShop.Cart.Dtos
{
    public static class Extension
    {
        public static CartItemReponse MapCartResponse(
            this CartItem item,
            string name,
            string imagePath,
            string platform,
            DateTime dateRelease,
            decimal basePrice,
            decimal currentPrice,
            List<string> genre,
            double rating,
            string publisher,
            string developer
            )
        {
            return new CartItemReponse(
                item.GameId,
                name,
                imagePath,
                platform,
                dateRelease,
                basePrice,
                currentPrice,
                genre,
                rating,
                publisher,
                developer,
                item.Quantity
            );
        }
    }
}