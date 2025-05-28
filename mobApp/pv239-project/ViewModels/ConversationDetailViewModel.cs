using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Models;

namespace pv239_project.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ConversationDetailViewModel : ObservableObject
{
    public int Id { get; init; } = -1;

    [ObservableProperty]
    public partial ConversationDetail? Conversation { get; set; }

    [ObservableProperty]
    public partial string MessageInput { get; set; } = string.Empty;

    public ConversationDetailViewModel()
    {
        Conversation = new ConversationDetail
        {
            Id = Guid.NewGuid(),
            Name = "User 1",
            Participants = new ObservableCollection<Guid>(),
            Messages = new ObservableCollection<Message>(),
        };

        Conversation.Messages = new ObservableCollection<Message>()
        {
            new Message 
            {
                Id = 1,
                Content = "Hello, how are you?",
                ConversationId = 1,
                SenderId = 1
            },
            new Message
            {
                Id = 1,
                Content = "I'm fine, thanks! And you?",
                ConversationId = 1,
                SenderId = 2
            }
        };
    }

    [RelayCommand]
    private Task SendMessage()
    {
        return Task.FromResult(Task.CompletedTask);
    }
}
