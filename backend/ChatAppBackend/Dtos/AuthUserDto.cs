namespace ChatAppBackend.Dtos;

public class AuthUserDto : UserDto
{
    public string Token { get; set; }
}