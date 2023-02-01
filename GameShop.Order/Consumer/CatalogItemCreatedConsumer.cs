using MassTransit;
using GameShop.Contract.Game;
using GameShop.Common;
using GameShop.Order.Entities;

namespace GameShop.Order.Consumer
{
    public class CatalogItemCreatedConsumer : IConsumer<GameCreated>
    {
        private readonly IRepository<CatalogItem> repository;

        public CatalogItemCreatedConsumer(IRepository<CatalogItem> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<GameCreated> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.Id);
            if (item != null)
            {
                return;
            }
            item = new CatalogItem(message.Id,message.Name, message.ImagePath, message.BasePrice, message.CurrentPrice);
            await repository.CreateAsync(item);
        }
    }
}