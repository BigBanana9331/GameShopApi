namespace GameShop.User.Contract
{
    public record UserUpdated(Guid Id, string UserName, string Email);
}