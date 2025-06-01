namespace pv239_project.Models;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int SenderId { get; set; }
    public bool IsOutgoing { get; set; }
    
    public DateTimeOffset MessageTime { get; set; }
    public string? ProfileImage { get; set; }
    public string Initials { get; set; }
}
