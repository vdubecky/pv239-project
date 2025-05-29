namespace ChatAppBackend.Dtos;

public class ConversationDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public IEnumerable<MessageDto>? Messages { get; set; }
    public IEnumerable<MemberDto>? Members { get; set; }
}