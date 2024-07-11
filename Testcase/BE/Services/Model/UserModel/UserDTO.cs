using System.ComponentModel.DataAnnotations;

namespace API.Model.UserModel
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        [MaxLength(10,ErrorMessage ="The Phone must be 10 number")]
        [MinLength(10, ErrorMessage = "The Phone must be 10 number")]
        public string? Phone { get; set; }
        public string? Image { get; set; }
    }
}
