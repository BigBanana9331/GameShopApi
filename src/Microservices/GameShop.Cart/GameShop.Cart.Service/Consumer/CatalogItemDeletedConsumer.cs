using GameShop.Cart.Contract;
using GameShop.Common;
using GameShop.Cart.Entities;
using MassTransit;
namespace GameShop.Cart.Consumer
{
    public class CatalogItemDeletedConsumer : IConsumer<GameDeleted>
    {
        private readonly IRepository<CatalogItem> repository;

        public CatalogItemDeletedConsumer(IRepository<CatalogItem> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<GameDeleted> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.Id);
            if (item == null)
            {
                return;
            }
            await repository.RemoveAsync(message.Id);
        }
    }
}