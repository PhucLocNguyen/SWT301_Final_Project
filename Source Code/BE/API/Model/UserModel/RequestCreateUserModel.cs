using System.ComponentModel.DataAnnotations;

namespace API.Model.UserModel
{
    public class RequestCreateUserModel
    {
        public string? Username { get; set; }
        
        public string? Email { get; set; }
      
        public string? Password { get; set; }
    }
}
