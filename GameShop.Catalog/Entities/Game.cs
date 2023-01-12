using GameShop.Common;

namespace GameShop.Catalog.Entities
{

    public class Game : IEntity
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Platform { get; }
        public string ImagePath { get; }
        public DateTime DateRelease { get; }
        public Decimal BasePrice { get; }
        public Decimal CurrentPrice { get; }
        public List<string> Genre { get; }
        public double Rating { get; }
        public string Publisher { get; }
        public string Developer { get; }

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
