namespace pv239_project.Models;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime SentTime { get; set; }
    public Guid UserId { get; set; }
    public Guid ConversationId { get; set; }
}
