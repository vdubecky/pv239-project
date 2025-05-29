using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppBackend.Entities;

[Table("Users")]
public class UserEntity
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public required string Firstname { get; set; }

    [Required, MaxLength(100)]
    public required string Surname { get; set; }

    [Required, EmailAddress, MaxLength(100)]
    public required string Email { get; set; }
    
    public string PasswordHash { get; set; }

    [MaxLength(512)]
    public string? ProfilePicture { get; set; }

    public ICollection<ConversationMember>? Conversations { get; init; }
    public ICollection<MessageEntity>? Messages { get; init; }
}