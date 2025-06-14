using CommunityToolkit.Mvvm.ComponentModel;

namespace pv239_project.Models;

public class User : ObservableObject, ICloneable
{
    public required int Id { get; set; }
    public required string Firstname { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public string? ProfilePicture { get; set; }
    
    public string Fullname => $"{Firstname} {Surname}";
    public string Initials => $"{Firstname[0]}{Surname[0]}";
    
    public object Clone()
    {
        return new User
        {
            Id = Id,
            Firstname = Firstname,
            Surname = Surname,
            Email = Email,
            ProfilePicture = ProfilePicture
        };
    }
}
