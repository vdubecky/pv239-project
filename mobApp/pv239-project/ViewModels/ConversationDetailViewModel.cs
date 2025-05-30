using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Client;
using pv239_project.Mappers;
using pv239_project.Models;
using pv239_project.Services.Interfaces;

namespace pv239_project.ViewModels;

[QueryProperty(nameof(ConversationName), nameof(ConversationName))]
[QueryProperty(nameof(ConversationId), nameof(ConversationId))]
[QueryProperty(nameof(ReceiverId), nameof(ReceiverId))]
public partial class ConversationDetailViewModel(IConversationClient conversationClient, IHubService hubService, IConversationsService conversationsService) : ObservableObject
{
    [ObservableProperty]
    public partial ConversationDetail? Conversation { get; set; }

    [ObservableProperty]
    public partial string MessageInput { get; set; } = string.Empty;
    
    [ObservableProperty]
    private string conversationName = "New Conversation";
    
    public int ConversationId { get; init; } = -1;
    public int ReceiverId { get; init; } = -1;

    private ConversationPreview? _preview;


    public async Task OnAppearing()
    {
        try
        {
            Conversation = (await DownloadConversation()).ConversationDtoToDetail();
            RegisterMessageHandler();
            
            _preview = conversationsService.Conversations
                .FirstOrDefault(c => c.ConversationId == Conversation.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($@"Error initializing conversation: {ex.Message}");
        }
    }
    
    public void OnDisappearing()
    {
        if (Conversation != null)
        {
            hubService.MessageHandler.Remove(Conversation.Id.ToString());
        }
    }

    private async Task<ConversationDto> DownloadConversation()
    {
        if (ConversationId != -1)
        {
            return await conversationClient.Conversation_GetConversationByIdAsync(ConversationId);
        }
        
        return await conversationClient.Conversation_GetConversationByMembersAsync(1, ReceiverId);
    }
    
    private void RegisterMessageHandler()
    {
        hubService.MessageHandler.Add(Conversation.Id.ToString(), message =>
        {                
            Conversation.Messages.Add(new Message
            {
                Content = message.Content,
                SenderId = message.SenderId
            });
        });
    }
    
    [RelayCommand]
    private async Task SendMessage()
    {
        if (string.IsNullOrEmpty(MessageInput))
        {
            return;
        }

        if (Conversation == null)
        {
            await CreateNewConversation();
        }
        else
        {
            await SendNewMessage();
        }

        MessageInput = string.Empty;
    }

    private async Task SendNewMessage()
    {
        Message message = new()
        {
            SenderId = 1,
            Content = MessageInput
        };

        await conversationClient.Conversation_SendMessageAsync(Conversation.Id, message.MessageToDto());

        UpdateLastMessageInPreview();
        Conversation.Messages.Add(message);
    }

    private async Task CreateNewConversation()
    {
        var conversationDto = new ConversationCreateDto
        {
            SenderId = 1,
            ReceiverId = ReceiverId,
            FirstMessage = MessageInput
        };
        
        var newConversationDto = await conversationClient.Conversation_CreateConversationAsync(conversationDto);
        Conversation = newConversationDto.ConversationDtoToDetail();
        
        _preview = Conversation.ConversationDetailToPreview(ConversationName, MessageInput);
        conversationsService.Conversations.Add(_preview);
        
        UpdateLastMessageInPreview();
        RegisterMessageHandler();
    }

    private void UpdateLastMessageInPreview()
    {
        if (_preview != null)
        {
            _preview.LastMessage = MessageInput;
        }
    }
}
