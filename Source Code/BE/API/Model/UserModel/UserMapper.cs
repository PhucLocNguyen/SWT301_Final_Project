using Repositories.Entity;
using System.Security.Cryptography;
using BCrypt.Net;

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
    }
}
