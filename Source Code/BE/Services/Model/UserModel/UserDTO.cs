namespace API.Model.UserModel
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
    }
}
