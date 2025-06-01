using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppBackend.Entities;

[Table("Messages")]
public class MessageEntity
{
    public int Id { get; init; }
    
    [Required, Range(1, int.MaxValue)]
    public required int SenderId { get; init; }
    
    [Required, Range(1, int.MaxValue)]
    public required int ConversationId { get; init; }
    
    [Required, MaxLength(255)]
    public required string Content { get; init; }
    
    public required string SentAt { get; init; }
    
    public UserEntity? SenderEntity { get; init; }
    public ConversationEntity? ConversationEntity { get; init; }
}