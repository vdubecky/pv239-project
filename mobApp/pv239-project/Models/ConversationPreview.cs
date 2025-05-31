using CommunityToolkit.Mvvm.ComponentModel;

namespace pv239_project.Models;

public partial class ConversationPreview : ObservableObject
{
    public int ConversationId { get; set; }
    public string Title { get; set; }

    [ObservableProperty] public partial string LastMessage { get; set; }
    [ObservableProperty] public partial DateTimeOffset LastMessageTime { get; set; }
    
    public string? ProfilePicture { get; set; }

    public string Initials
    {
        get
        {
            var splitedTitle = Title.Split([' '], 2, StringSplitOptions.RemoveEmptyEntries);
            return splitedTitle.Length < 2 ? $"{Title[0]}" : $"{splitedTitle[0][0]}{splitedTitle[1][0]}";
        }
    }
}
