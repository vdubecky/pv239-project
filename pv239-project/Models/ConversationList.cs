using CommunityToolkit.Mvvm.ComponentModel;

namespace pv239_project.Models;

public class ConversationList : ObservableObject
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string LastMessage { get; set; }
    public DateTime LastActivity { get; set; }
}
