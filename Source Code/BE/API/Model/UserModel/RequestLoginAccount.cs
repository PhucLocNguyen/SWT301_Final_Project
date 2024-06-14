using System.ComponentModel.DataAnnotations;

namespace API.Model.UserModel
{
    public class RequestLoginAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
