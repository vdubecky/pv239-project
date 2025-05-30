using Microsoft.AspNetCore.SignalR.Client;
using pv239_project.Client;
using pv239_project.Mappers;
using pv239_project.Services.Interfaces;


namespace pv239_project.Services;

public class HubService : IHubService
{
    public static readonly string URL = $"{MauiProgram.BASE_URL}chatAppHub?userId=@";
    public static readonly string SIGNALR_NEW_MESSAGE = "SendMessage";
    public static readonly string SIGNALR_NEW_CONVERSATION = "SendConversation";
    
    public required Dictionary<string, Action<CreateMessageDto>> MessageHandler { get; set; } = new();

    private readonly HubConnection _connection;
    private readonly IConversationsService _conversationsService;


    public HubService(IConversationsService conversations)
    {
        _conversationsService = conversations;
        _connection = new HubConnectionBuilder()
            .WithUrl(URL.Replace("@", "1"))
            .WithAutomaticReconnect()
            .Build();
    }

    public async Task Start()
    {
        await _connection.StartAsync();

        _connection.On<int, CreateMessageDto>(SIGNALR_NEW_MESSAGE, OnNewMessage);
        _connection.On<ConversationPreviewDto>(SIGNALR_NEW_CONVERSATION, OnNewConversation);
    }
    
    private void OnNewMessage(int conversationId, CreateMessageDto messageDto)
    {
        var conversation = _conversationsService.Conversations
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
        _conversationsService.Conversations.Add(previewDto.PreviewDtoToPreview());
    }
}