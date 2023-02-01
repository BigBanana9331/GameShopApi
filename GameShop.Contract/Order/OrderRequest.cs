namespace GamesShop.Contract.Order
{
    public record OrderRequest(
        Guid UserId,
        decimal Discounted,
        DateTime PurchasedDate
    );
}