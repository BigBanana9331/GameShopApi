using MassTransit;
using GameShop.Order.Contract;
using GameShop.Common;
using GameShop.Order.Entities;

namespace GameShop.Order.Consumer
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