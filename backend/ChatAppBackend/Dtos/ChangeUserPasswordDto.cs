using System.ComponentModel.DataAnnotations;

namespace ChatAppBackend.Dtos
{
    public class ChangeUserPasswordDto
    {
        [Required, MinLength(6)]
        public string OldPassword { get; set; }

        [Required, MinLength(6)]
        public string NewPassword { get; set; }

        [Required, MinLength(6)]
        public string NewPasswordConfirm { get; set; }
    }
}
