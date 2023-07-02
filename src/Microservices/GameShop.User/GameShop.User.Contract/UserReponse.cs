namespace GameShop.User.Contract
{
    public record UserResponse(
        Guid Id,
        string UserName,
        string Email,
        string PasswordHash,
        string PasswordSalt,
        string Role,
        string PhoneNumber,
        string AvatarPath
    );
}