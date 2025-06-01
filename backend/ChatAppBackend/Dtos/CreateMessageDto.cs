using System.ComponentModel.DataAnnotations;

namespace ChatAppBackend.Dtos;

public class CreateMessageDto
{
    [Range(1, int.MaxValue)]
    public required int SenderId { get; set; }
        
    [Required, MaxLength(255)]
    public required string Content { get; set; }
    
    public string? LastMessageDate { get; set; }
}