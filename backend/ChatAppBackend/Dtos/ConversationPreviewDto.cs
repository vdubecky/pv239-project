namespace ChatAppBackend.Dtos;

public class ConversationPreviewDto
{
    public int ConversationId { get; set; }
    public required string Name { get; set; }
    public required string LastMessage { get; set; }

    public string? ProfilePicture { get; set; }
}