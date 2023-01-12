namespace GameShop.Contract.User
{
    public record UserResponse(
        Guid Id,
        string UserName,
        string Email
    );
}