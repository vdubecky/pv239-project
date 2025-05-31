namespace pv239_project.Models;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int SenderId { get; set; }
    public int ConversationId { get; set; }
    public bool IsOutgoing { get; set; }
}
