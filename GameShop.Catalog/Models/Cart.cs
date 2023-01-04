namespace GamesShop.Models;

public class Cart
{
    public Guid UserId { get; }
    public List<Game>? Games { get; }
    public float Total { get; }
    public float Discount { get; }

    public Cart(Guid userId, List<Game> games, float total, float discount)
    {
        UserId = userId;
        Games = games ?? new();
        Total = total;
        Discount = discount;
    }

    public static Cart Create(
        Guid userId,
        List<Game> games,
        float total,
        float discount
    )
    {
        return new Cart(
            userId,
            games ?? new(),
            total,
            discount
        );
    }
}