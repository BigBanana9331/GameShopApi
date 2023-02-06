using GameShop.Common;
using GameShop.Contract.User;

namespace GameShop.User.Entities
{
    public class UserAccount : IEntity
    {
        public Guid Id { get; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string AvatarPath { get; set; }

        public UserAccount(Guid id, string userName, string email, string phoneNumber, string passWord, string avatarPath)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = passWord;
            AvatarPath = avatarPath;
        }
        public static UserAccount Create(string userName, string email, string phoneNumber, string passWord, string avatarPath, Guid? id = null)
        {
            return new UserAccount(id ?? Guid.NewGuid(), userName, email, phoneNumber, passWord, avatarPath);
        }
        public static UserAccount MapUserRequest(UserRequest request, Guid? id = null)
        {
            return UserAccount.Create(request.UserName, request.Email, request.PhoneNumber, request.Password, request.AvatarPath, id);
        }

        public static UserResponse MapUserResponse(UserAccount user)
        {
            return new UserResponse(user.Id, user.UserName, user.Email, user.PhoneNumber, user.Password, user.AvatarPath);
        }
    }
}