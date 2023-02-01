using MassTransit;
using GameShop.Contract.User;
using GameShop.Common;
using GameShop.Order.Entities;

namespace GameShop.Order.Consumer
{
    public class CustomerDeletedConsumer : IConsumer<UserDeleted>
    {
        private readonly IRepository<Customer> repository;

        public CustomerDeletedConsumer(IRepository<Customer> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<UserDeleted> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.Id);
            if (item != null)
            {
                await repository.RemoveAsync(message.Id);
            }
        }
    }
}