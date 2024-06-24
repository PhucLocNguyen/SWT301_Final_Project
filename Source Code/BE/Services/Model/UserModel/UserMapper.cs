using Repositories.Entity;
using System.Security.Cryptography;
using BCrypt.Net;
using SWP391Project.Services.LoginGoogleSystem;

namespace API.Model.UserModel
{
    public static class UserMapper
    {
        public static Users toUserEntity(this RequestRegisterAccount requestRegisterAccount, Role role)
        {
            return new Users()
            {
                Username = requestRegisterAccount.Username,
                Name = requestRegisterAccount.Username,
                Email = requestRegisterAccount.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(requestRegisterAccount.Password),
                RoleId = role.RoleId,
                Role = role,
            }; 
        }

        public static UserDTO toUserDTO(this Users user)
        {
            return new UserDTO()
            {
                UserId = user.UsersId,
                Username = user.Username,
                Name = user.Name,
                Email = user.Email,
            };
        }
        public static Users FromUserInfoGoogleToUserEntity(this UserInfo user, Role role)
        {

            return new Users()
            {
                Username = user.Email,
                Email = user.Email,
                Name = user.FamilyName + " " + user.GivenName,
                Image = user.Picture,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                RoleId = user.RoleId,
                Role = role
            };
        }
    }
}
