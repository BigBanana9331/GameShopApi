using GameShop.Common;

namespace GameShop.Cart.Entities
{
    public class Customer : IEntity
    {
        public Guid Id { get; set; }

        public Customer(Guid id)
        {
            Id = id;
        }
        //     public static Customer Create(
        //         Guid? id = null

        //     )
        //     {
        //         return new Customer(
        //             id ?? Guid.NewGuid()
        //         );
        //     }
        // }
    }
}