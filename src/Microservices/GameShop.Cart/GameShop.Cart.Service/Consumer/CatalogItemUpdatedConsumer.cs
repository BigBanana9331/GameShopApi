using MassTransit;
using GameShop.Common;
using GameShop.Cart.Entities;
using GameShop.Cart.Contract;
namespace GameShop.Cart.Consumer
{
    public class CatalogItemUpdatedConsumer : IConsumer<GameUpdated>
    {
        private readonly IRepository<CatalogItem> repository;

        public CatalogItemUpdatedConsumer(IRepository<CatalogItem> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<GameUpdated> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.Id);
            if (item == null)
            {
                item = new CatalogItem(message.Id, message.Name, message.ImagePath, message.BasePrice, message.CurrentPrice);
                await repository.CreateAsync(item);
            }
            else
            {
                item.Name = message.Name;
                item.ImagePath = message.ImagePath;
                item.BasePrice = message.BasePrice;
                item.CurrentPrice = message.CurrentPrice;
                await repository.UpdateAsync(item);
            }
        }
    }
}