using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using pv239_project.Client;
using pv239_project.Configuration;
using pv239_project.Helpers;
using pv239_project.Mappers;
using pv239_project.Services.Interfaces;


namespace pv239_project.Services;

public class HubService(IConversationsService conversations, IUserService userService, INotificationManagerService notificationManager, IOptions<ApiOptions> options) : IHubService
{
    public const string SignalRNewMessage = "SendMessage";
    public const string SignalRNewConversation = "SendConversation";
    
    public required Dictionary<string, Action<CreateMessageDto>> MessageHandler { get; set; } = new();

    private HubConnection? _connection;
    
    
    public async Task Start()
    {
        string url = $"{options.Value.ApiUrl}chatAppHub?userId={userService.CurrentUserId.ToString()}";
        _connection = new HubConnectionBuilder()
            .WithUrl(url)
            .WithAutomaticReconnect()
            .Build();
        
        await _connection.StartAsync();

        _connection.On<int, CreateMessageDto>(SignalRNewMessage, OnNewMessage);
        _connection.On<ConversationPreviewDto>(SignalRNewConversation, OnNewConversation);
    }
    
    private void OnNewMessage(int conversationId, CreateMessageDto messageDto)
    {
        var conversation = conversations.Conversations
            .FirstOrDefault(c => c.ConversationId == conversationId);
        
        if (conversation != null)
        {
            conversation.LastMessage = messageDto.Content;
            conversation.LastMessageTime = messageDto.LastMessageDate.ParseTime();
            conversations.SortConversationsByLastMessage(conversation);
        }

        if (!App.IsInForeground)
        {
            notificationManager.SendNotification(conversation.Title, conversation.LastMessage);    
        }
        
        MessageHandler.TryGetValue(conversationId.ToString(), out Action<CreateMessageDto>? handler);
        handler?.Invoke(messageDto);
    }

    private void OnNewConversation(ConversationPreviewDto previewDto)
    {
        var preview = previewDto.PreviewDtoToPreview();
        conversations.Conversations.Add(preview);
        conversations.SortConversationsByLastMessage(preview);
    }
}