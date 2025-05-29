using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ChatAppBackend.Entities;

[Table("Conversations")]
public class ConversationEntity
{
    public int Id { get; init; }

    [Required, MaxLength(100)]
    public required string Name { get; init; }
        
    public int? LastMessageId { get; set; }
        
    [ForeignKey("LastMessageId")]
    public MessageEntity? LastMessage { get; set; }

    public ICollection<MessageEntity>? Messages { get; set; }
    public ICollection<ConversationMember>? Members { get; set; }
}
