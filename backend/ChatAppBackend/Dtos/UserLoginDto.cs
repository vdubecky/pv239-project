using System.ComponentModel.DataAnnotations;

namespace ChatAppBackend.Dtos
{
    public class UserLoginDto
    {
        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
