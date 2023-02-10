using System.ComponentModel.DataAnnotations;
using GameShop.Common;
using GameShop.Contract.User;

namespace GameShop.User.Entities
{
    public class UserAccount : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string PasswordSalt { get; set; }
        public string Role { get; set; }
        [StringLength(10)]
        public string? PhoneNumber { get; set; }
        public string? AvatarPath { get; set; }


        private UserAccount
        (
            Guid id,
            string userName,
            string email,
            string passwordHash,
            string passwordSalt,
            string role,
            string? phoneNumber,
            string? avatarPath

        )
        {
            Id = id;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Role = role;
            AvatarPath = avatarPath;
            PhoneNumber = phoneNumber;

        }
        public static UserAccount Create
        (
            string userName,
            string email,
            string passwordHash,
            string passwordSalt,
            Guid? id = null,
            string role = "user",
            string? phoneNumber = null,
            string? avatarPath = null


        )
        {
            return new UserAccount
            (
                id ?? Guid.NewGuid(),
                userName,
                email,
                passwordHash,
                passwordSalt,
                role,
                phoneNumber,
                avatarPath

            );
        }
        public static UserAccount MapRegisterUserRequest
        (
            RegisterUserRequest request, 
            string passwordHash, 
            string passwordSalt, 
            Guid? id = null
        )
        {
            return UserAccount.Create
            (
                request.UserName,
                request.Email,
                passwordHash,
                passwordSalt,
                id ?? Guid.NewGuid()
            );
        }
        public static UserAccount MapUserRequest
        (
            UserRequest request, 
            string passwordHash, 
            string passwordSalt, 
            Guid? id = null
        )
        {
            return UserAccount.Create
            (
                request.UserName,
                request.Email,
                passwordHash,
                passwordSalt,
                id ?? Guid.NewGuid(),
                request.Role,
                request.PhoneNumber,
                request.AvatarPath
            );
        }

        public static UserResponse MapUserResponse(UserAccount user)
        {
            return new UserResponse
            (
                user.Id,
                user.UserName,
                user.Email,
                user.PasswordHash,
                user.PasswordSalt,
                user.Role,
                user.PhoneNumber ?? string.Empty,
                user.AvatarPath ?? string.Empty
            );
        }
    }
}