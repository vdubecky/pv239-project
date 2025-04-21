using System.ComponentModel.DataAnnotations;

namespace ChatAppBackend.Dtos
{
    public class UserUpdateDto
    {
        [Required, MaxLength(100)]
        public string Firstname { get; set; }

        [Required, MaxLength(100)]
        public string Surname { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(512)]
        public string ProfilePicture { get; set; }
    }
}
