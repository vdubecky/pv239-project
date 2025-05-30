using System.Collections.ObjectModel;
using pv239_project.Client;
using pv239_project.Mappers;
using pv239_project.Models;
using pv239_project.Services.Interfaces;

namespace pv239_project.Services;

public class ConversationsService(IConversationClient conversationClient) : IConversationsService
{
    public ObservableCollection<ConversationPreview> Conversations { get; } = new();
    
    
    public async Task Init()
    {
        var dtos = await conversationClient.Conversation_GetConversationsWithCurrentUserAsync(1);
        
        foreach (var dto in dtos)
        {
            Conversations.Add(dto.PreviewDtoToPreview());
        }
    }
}