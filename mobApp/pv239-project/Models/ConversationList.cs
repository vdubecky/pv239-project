using CommunityToolkit.Mvvm.ComponentModel;

namespace pv239_project.Models;

public class ConversationList : ObservableObject
{
    public int ConversationId { get; set; }
    public string Title { get; set; }
    public string LastMessage { get; set; }
}
