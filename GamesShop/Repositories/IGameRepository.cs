using GamesShop.Entities;

namespace GamesShop.Repositories
{
    public interface IGameRepository
    {
        Task CreateAsync(Game entity);
        Task<IReadOnlyCollection<Game>> GetAllAsync();
        Task<Game> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Game entity);
    }
}