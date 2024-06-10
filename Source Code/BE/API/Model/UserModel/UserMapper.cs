using Repositories.Entity;

namespace API.Model.UserModel
{
    public static class UserMapper
    {
        public static AppUser toAppUser(this RequestCreateUserModel requestCreateUserModel)
        {
            return new AppUser()
            {
                Email = requestCreateUserModel.Email,
                UserName = requestCreateUserModel.Username,
            }; 
        }

        public static UserDTO toUserDTO(this AppUser user)
        {
            return new UserDTO()
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email
            };
        }
    }
}
