using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using pv239_project.Models;

namespace pv239_project.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ConversationDetailViewModel  : ObservableObject
{
    public Guid Id { get; init; } = Guid.Empty;
    
    [ObservableProperty]
    public partial ConversationDetail? Conversation { get; set; }

    public ConversationDetailViewModel()
    {
        Conversation = new ConversationDetail
        {
            Id = Guid.NewGuid(),
            Name = "User 1",
            Participants = new ObservableCollection<Guid>(),
            Messages = [],
        };
    }
}
