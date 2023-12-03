using GameShop.Cart.Contract;
using GameShop.Common;
using GameShop.Cart.Entities;
using MassTransit;
namespace GameShop.Cart.Consumer
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
            item = new CatalogItem(message.Id, message.Name, message.ImagePath, message.BasePrice, message.CurrentPrice);
            await repository.CreateAsync(item);
        }
    }
}