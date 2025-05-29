using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppBackend.Entities;

[Table("ConversationMembers")]
public class ConversationMember
{
    public int Id { get; init; }
    public int ConversationId { get; init; }
    
    [Required, Range(1, int.MaxValue)]
    public required int UserId { get; init; }
    
    public UserEntity? UserEntity { get; init; }
    public ConversationEntity? ConversationEntity { get; init; }
}