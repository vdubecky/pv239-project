using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using pv239_project.Client;
using pv239_project.Helpers;
using pv239_project.Mappers;
using pv239_project.Models;
using pv239_project.Resources.i18n;
using pv239_project.Services.Interfaces;

namespace pv239_project.ViewModels;

[QueryProperty(nameof(ReceiverId), nameof(ReceiverId))]
public partial class ConversationDetailViewModel(IConversationClient conversationClient, IHubService hubService, IConversationsService conversationsService, IUserService userService) : ObservableObject
{
    [ObservableProperty]
    public partial ConversationDetail? Conversation { get; set; }

    [ObservableProperty]
    public partial string MessageInput { get; set; } = string.Empty;

    [ObservableProperty]
    public partial ConversationPreview Preview { get; set; } = conversationsService.SelectedConversation;
    
    public int ReceiverId { get; init; } = -1;


    public async Task OnAppearing()
    {
        try
        {
            Conversation = (await DownloadConversation()).ConversationDtoToDetail(userService.CurrentUserId, Preview);
            RegisterMessageHandler();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
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
        if (Preview.ConversationId != -1)
        {
            return await conversationClient.Conversation_GetConversationByIdAsync(Preview.ConversationId);
        }
        
        var conversation = await conversationClient.Conversation_GetConversationByMembersAsync(userService.CurrentUserId, ReceiverId);
        conversationsService.SelectedConversation = conversationsService.Conversations
            .First(c => c.ConversationId == conversation.Id);
        
        return conversation;
    }
    
    private void RegisterMessageHandler()
    {
        hubService.MessageHandler.Add(Conversation.Id.ToString(), message =>
        {    
            Conversation.Messages.Add(new Message
            {
                Content = message.Content,
                SenderId = message.SenderId,
                IsOutgoing = message.SenderId == userService.CurrentUserId,
                Initials = Preview.Initials,
                ProfileImage = message.SenderId != userService.CurrentUserId ? Preview.ProfilePicture : null,
                MessageTime = message.LastMessageDate.ParseTime()
            });
        });
    }
    
    [RelayCommand]
    private async Task SendMessage()
    {
        string input = MessageInput.Trim();
        
        if (string.IsNullOrEmpty(input))
        {
            return;
        }

        try
        {
            if (Conversation == null)
            {
                await CreateNewConversation(input);
            }
            else
            {
                await SendNewMessage(input);
            }
        }
        catch (Exception ex)
        {
            await Toast.Make(AppTexts.ConversationDetailPage_FailedToSendMessageError).Show();
        }
        
        MessageInput = string.Empty;
    }

    private async Task SendNewMessage(string content)
    {
        Message message = new()
        {
            SenderId = userService.CurrentUserId,
            Content = content,
            IsOutgoing = true,
            MessageTime = DateTimeOffset.Now
        };
        
        await conversationClient.Conversation_SendMessageAsync(Conversation.Id, message.MessageToDto());
            
        UpdateLastMessageInPreview(content);
        Conversation.Messages.Add(message);
    }

    private async Task CreateNewConversation(string content)
    {
        var conversationDto = new ConversationCreateDto
        {
            SenderId = userService.CurrentUserId,
            ReceiverId = ReceiverId,
            FirstMessage = content,
        };
        
        var newConversationDto = await conversationClient.Conversation_CreateConversationAsync(conversationDto);
        Conversation = newConversationDto.ConversationDtoToDetail(userService.CurrentUserId, Preview);

        conversationsService.SelectedConversation.ConversationId = Conversation.Id;
        conversationsService.SelectedConversation.LastMessageTime = DateTimeOffset.Now;
        conversationsService.Conversations.Add(conversationsService.SelectedConversation);
        
        UpdateLastMessageInPreview(content);
        RegisterMessageHandler();
    }

    private void UpdateLastMessageInPreview(string content)
    {
        conversationsService.SelectedConversation.LastMessage = content;
        conversationsService.SelectedConversation.LastMessageTime = DateTime.Now;
        conversationsService.SortConversationsByLastMessage(conversationsService.SelectedConversation);
    }
}
