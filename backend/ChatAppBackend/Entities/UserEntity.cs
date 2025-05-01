using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppBackend.Entities
{
    [Table("Users")]
    public class UserEntity
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Firstname { get; set; }

        [Required, MaxLength(100)]
        public string Surname { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [MaxLength(512)]
        public string ProfilePicture { get; set; }
    }
}
