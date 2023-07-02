using GameShop.Common;

namespace GameShop.Cart.Entities
{
    public class CatalogItem : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public Decimal BasePrice { get; set; }
        public Decimal CurrentPrice { get; set; }

        public CatalogItem(
            Guid id,
            string name,
            string imagePath,
            Decimal basePrice,
            Decimal currentPrice
        )
        {
            Id = id;
            Name = name;
            ImagePath = imagePath;
            BasePrice = basePrice;
            CurrentPrice = currentPrice;
        }
        // public static CatalogItem Create(
        //     string name,
        //     string imagePath,
        //     Decimal basePrice,
        //     Decimal currentPrice,
        //     Guid? id = null
        // )
        // {
        //     return new CatalogItem(
        //         id ?? Guid.NewGuid(),
        //         name,
        //         imagePath,
        //         basePrice,
        //         currentPrice
        //     );
        // }
    }
}