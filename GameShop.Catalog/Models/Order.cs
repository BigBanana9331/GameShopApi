namespace GameShop.Catalog.Models;

public class Order{
    public Guid Id {get;}
    public Guid UserId {get;}
    public List<Game>? Games {get;}
    public float Total {get;}
    public float Discount {get;}
    public DateTime DateTimeOrders {get;}

    public Order(Guid id, Guid userId, List<Game> games, float total, float discount, DateTime dateTimeOrders)
    {
        Id = id;
        UserId = userId;
        Games = games ?? new();
        Total = total;
        Discount = discount;
        DateTimeOrders = dateTimeOrders;
    }

    public static Order Create(
        Guid userId, 
        List<Game> games, 
        float total, 
        float discount, 
        DateTime dateTimeOrders,
        Guid? id = null
    )
    {
        return new Order(
            id ?? new Guid(),
            userId,
            games ?? new(),
            total,
            discount,
            dateTimeOrders
        );
    }
}