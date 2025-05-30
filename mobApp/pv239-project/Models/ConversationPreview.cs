using CommunityToolkit.Mvvm.ComponentModel;

namespace pv239_project.Models;

public partial class ConversationPreview : ObservableObject
{
    public int ConversationId { get; set; }
    public string Title { get; set; }
    
    [ObservableProperty]
    public partial string LastMessage { get; set; }
}
