using MassTransit;
using GameShop.Order.Contract;
using GameShop.Common;
using GameShop.Order.Entities;

namespace GameShop.Order.Consumer
{
    public class CustomerCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly IRepository<Customer> repository;

        public CustomerCreatedConsumer(IRepository<Customer> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.Id);
            if (item != null)
            {
                return;
            }
            item = new Customer(message.Id);
            await repository.CreateAsync(item);
        }
    }
}