namespace GameShop.Contract.User
{
    public record UserResponse(
        Guid Id,
        string UserName,
        string Email,
        string PhoneNumber,
        string Password,
        string AvatarPath
    );
}