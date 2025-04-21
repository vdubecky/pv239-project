using System.ComponentModel.DataAnnotations;

namespace ChatAppBackend.Dtos
{
    public class UserLoginDto
    {
        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
