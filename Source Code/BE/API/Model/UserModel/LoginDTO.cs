using System.ComponentModel.DataAnnotations;

namespace API.Model.UserModel
{
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
