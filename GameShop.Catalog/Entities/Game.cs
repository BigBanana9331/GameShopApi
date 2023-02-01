using GameShop.Common;

namespace GameShop.Catalog.Entities
{

    public class Game : IEntity
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateRelease { get; set; }
        public Decimal BasePrice { get; set; }
        public Decimal CurrentPrice { get; set; }
        public List<string> Genre { get; set; }
        public double Rating { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }

        public Game(
            Guid id,
            string name,
            string imagePath,
            string platform,
            DateTime dateRelease,
            Decimal basePrice,
            Decimal currentPrice,
            List<string> genre,
            double rating,
            string publisher,
            string developer
        )
        {
            Id = id;
            Name = name;
            ImagePath = imagePath;
            Platform = platform;
            DateRelease = dateRelease;
            BasePrice = basePrice;
            CurrentPrice = currentPrice;
            Genre = genre;
            Rating = rating;
            Publisher = publisher;
            Developer = developer;
        }

        public static Game Create(
            string name,
            string imagePath,
            string platform,
            DateTime dateRelease,
            Decimal basePrice,
            Decimal currentPrice,
            List<string> genre,
            double rating,
            string publisher,
            string developer,
            Guid? id = null
        )
        {
            return new Game(
                id ?? Guid.NewGuid(),
                name,
                imagePath,
                platform,
                dateRelease,
                basePrice,
                currentPrice,
                genre,
                rating,
                publisher,
                developer
            );
        }
    }
}
