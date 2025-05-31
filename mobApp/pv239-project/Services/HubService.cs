using Microsoft.AspNetCore.SignalR.Client;
using pv239_project.Client;
using pv239_project.Mappers;
using pv239_project.Services.Interfaces;


namespace pv239_project.Services;

public class HubService(IConversationsService conversations, IUserService userService) : IHubService
{
    public const string URL = $"{MauiProgram.BaseUrl}chatAppHub?userId=@";
    public const string SIGNALR_NEW_MESSAGE = "SendMessage";
    public const string SIGNALR_NEW_CONVERSATION = "SendConversation";
    
    public required Dictionary<string, Action<CreateMessageDto>> MessageHandler { get; set; } = new();

    private HubConnection? _connection;
    

    public async Task Start()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl(URL.Replace("@", userService.CurrentUserId.ToString()))
            .WithAutomaticReconnect()
            .Build();
        
        await _connection.StartAsync();

        _connection.On<int, CreateMessageDto>(SIGNALR_NEW_MESSAGE, OnNewMessage);
        _connection.On<ConversationPreviewDto>(SIGNALR_NEW_CONVERSATION, OnNewConversation);
    }
    
    private void OnNewMessage(int conversationId, CreateMessageDto messageDto)
    {
        var conversation = conversations.Conversations
            .FirstOrDefault(c => c.ConversationId == conversationId);
            
        if (conversation != null)
        {
            conversation.LastMessage = messageDto.Content;
        }
             
        MessageHandler.TryGetValue(conversationId.ToString(), out Action<CreateMessageDto>? handler);
        handler?.Invoke(messageDto);
    }

    private void OnNewConversation(ConversationPreviewDto previewDto)
    {
        conversations.Conversations.Add(previewDto.PreviewDtoToPreview());
    }
}