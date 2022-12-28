using GamesShop.Entities;
using MongoDB.Driver;

namespace GamesShop.Repositories
{
    public class GameRepository : IGameRepository
    {
        private const string collectionName = "games";
        private readonly IMongoCollection<Game>? dbCollection;
        private readonly FilterDefinitionBuilder<Game> filterBuilder = Builders<Game>.Filter;

        public GameRepository(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<Game>(collectionName);
        }
        public async Task<IReadOnlyCollection<Game>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Game> GetAsync(Guid id)
        {
            FilterDefinition<Game> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Game entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await dbCollection.InsertOneAsync(entity);
        }
        public async Task UpdateAsync(Game entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            FilterDefinition<Game> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }
        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<Game> filter = filterBuilder.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}