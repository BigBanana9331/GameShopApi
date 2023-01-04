namespace GamesShop.Cart{
    // public record GrantCart(Guid UserId, Guid GamesId, int Quantity, decimal total);
    // public record GrantCartRequest(Guid UserId, Guid GamesId, int Quantity, decimal total);
    // public record CartItemReponse(Guid GamesId, int Quantity, decimal total);
    public record GrantCartRequest(Guid UserId, Guid GamesId, int Quantity);
    public record CartItemReponse(Guid GamesId, int Quantity);
}