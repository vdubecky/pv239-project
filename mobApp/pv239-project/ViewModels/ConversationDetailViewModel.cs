using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Models;

namespace pv239_project.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ConversationDetailViewModel : ObservableObject
{
    public Guid Id { get; init; } = Guid.Empty;

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
            Messages = [],
        };
    }

    [RelayCommand]
    private Task SendMessage()
    {
        if (string.IsNullOrWhiteSpace(MessageInput) || Conversation is null)
        {
            return Task.FromResult(Task.CompletedTask);
        }

        var newMessage = new Message()
        {
            Id = Guid.NewGuid(),
            Text = MessageInput,
            SentTime = DateTime.Now,
            UserId = Guid.NewGuid(),
            ConversationId = Guid.NewGuid(),
        };

        Conversation?.Messages.Add(newMessage);
        MessageInput = string.Empty;
        return Task.FromResult(Task.CompletedTask);
    }
}
