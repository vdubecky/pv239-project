using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Client;
using pv239_project.Mappers;
using pv239_project.Models;
using pv239_project.Services.Interfaces;


namespace pv239_project.ViewModels;

[QueryProperty(nameof(ConversationId), nameof(ConversationId))]
[QueryProperty(nameof(ReceiverId), nameof(ReceiverId))]
public partial class ConversationDetailViewModel(IConversationClient conversationClient, IHubService hubService) : ObservableObject
{
    public int ConversationId { get; init; } = -1;
    public int ReceiverId { get; init; } = -1;

    [ObservableProperty]
    public partial ConversationDetail? Conversation { get; set; }

    [ObservableProperty]
    public partial string MessageInput { get; set; } = string.Empty;


    public async Task OnStart()
    {
        try
        {
            ConversationDto conversationDto;
            if (ConversationId != -1)
            {
                conversationDto = await conversationClient.Conversation_GetConversationByIdAsync(ConversationId);
            }
            else
            {
                conversationDto = await conversationClient.Conversation_GetConversationByMembersAsync(1, ReceiverId);
            }

            Conversation = conversationDto.ConversationDtoToDetail();
            hubService.MessageHandlers.Add(Conversation.Id.ToString(), (message) =>
            {                
                Conversation.Messages.Add(new Message()
                {
                    Content = message.Content,
                    SenderId = message.SenderId
                });
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }   
    }

    public async Task OnDestroy()
    {
        hubService.MessageHandlers.Remove(Conversation.Id.ToString());        
    }

    [RelayCommand]
    private async Task SendMessage()
    {
        if(string.IsNullOrEmpty(MessageInput))
        {
            return;
        }

        if (Conversation == null)
        {
            await CreateNewConversation();
            return;
        }

        Message message = new()
        {
            SenderId = MauiProgram.USER_ID,
            Content = MessageInput
        };

        await conversationClient.Conversation_SendMessageAsync(Conversation.Id, message.MessageToDto());

        Conversation.Messages.Add(message);
        MessageInput = string.Empty;
    }

    private async Task CreateNewConversation()
    {
        ConversationCreateDto conversationCreateDto = new()
        {
            SenderId = MauiProgram.USER_ID,
            ReceiverId = ReceiverId,
            FirstMessage = MessageInput
        };

        ConversationDto conversationDto = await conversationClient.Conversation_CreateConversationAsync(conversationCreateDto);
        Conversation = conversationDto.ConversationDtoToDetail();
        MessageInput = string.Empty;
    }
}
