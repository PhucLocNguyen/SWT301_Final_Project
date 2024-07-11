using Repositories.Entity;
using System.ComponentModel.DataAnnotations;

namespace API.Model.UserModel
{
    public class RequestRegisterAccount
    {
        [Required]
        public string? Username { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
        [Required]
        public string? PasswordConfirm { get; set; }

        public string? VerifyEmail { get; set; }

        public DateTime? Duration { get; set; }

    }
}
