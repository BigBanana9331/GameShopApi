using GameShop.Common;

namespace GameShop.User.Entities
{
    public class UserAcount : IEntity
    {
        public Guid Id { get; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string AvatarPath { get; set; }

        public UserAcount(Guid id, string userName, string email, string phoneNumber, string passWord, string avatarPath)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = passWord;
            AvatarPath = avatarPath;
        }
        public static UserAcount Create(string userName, string email, string phoneNumber, string passWord, string avatarPath, Guid? id = null)
        {
            return new UserAcount(id ?? Guid.NewGuid(), userName, email, phoneNumber, passWord, avatarPath);
        }
    }
}