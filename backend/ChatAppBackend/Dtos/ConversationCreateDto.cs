using System.ComponentModel.DataAnnotations;

namespace ChatAppBackend.Dtos;

public class ConversationCreateDto
{
    [Range(1, int.MaxValue)]
    public required int SenderId { get; set; }
        
    [Range(1, int.MaxValue)]
    public required int ReceiverId { get; set; }
        
    [Required, MaxLength(255)]
    public required string FirstMessage { get; set; }
}