using System.ComponentModel.DataAnnotations;

namespace ChatAppBackend.Dtos;

public class UserRegisterDto : UserUpdateDto
{
    [Required, MinLength(6)]
    public string Password { get; set; }
}