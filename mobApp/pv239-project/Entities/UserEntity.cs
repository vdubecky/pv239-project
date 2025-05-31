namespace pv239_project.Entities;

public class UserEntity : EntityBase
{
    public int UserId { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? ProfilePicture { get; set; }
}