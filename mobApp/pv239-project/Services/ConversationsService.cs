using System.Collections.ObjectModel;
using pv239_project.Client;
using pv239_project.Mappers;
using pv239_project.Models;
using pv239_project.Services.Interfaces;

namespace pv239_project.Services;

public class ConversationsService(IConversationClient conversationClient, IUserService userService) : IConversationsService
{
    public ObservableCollection<ConversationPreview> Conversations { get; } = new();
    public ConversationPreview? SelectedConversation { get; set; }


    public async Task Init()
    {
        var dtos = await conversationClient
            .Conversation_GetConversationsWithCurrentUserAsync(userService.CurrentUserId);
        
        foreach (var dto in dtos)
        {
            Conversations.Add(dto.PreviewDtoToPreview());
        }
    }
}