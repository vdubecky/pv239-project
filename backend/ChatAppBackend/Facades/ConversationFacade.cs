using ChatAppBackend.Dtos;
using ChatAppBackend.Entities;
using ChatAppBackend.Hubs;
using ChatAppBackend.Mappers;
using ChatAppBackend.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppBackend.Facades;

public class ConversationFacade(ConversationService conversationService, MessageService messageService, IHubContext<ChatAppHub> hub)
{
    public async Task<ConversationDto> CreateConversation(ConversationCreateDto conversationDto)
    {
        var conversationEntityId = await conversationService.CreateConversation(conversationDto.ReceiverId, conversationDto.SenderId);
        await messageService.CreateMessage(conversationDto.SenderId, conversationEntityId, conversationDto.FirstMessage);

        var conversation = await conversationService.GetConversation(conversationEntityId);
        ChatAppHub.Users.TryGetValue(conversationDto.ReceiverId.ToString(), out var clientId);

        if (clientId != null)
        {
            hub.Clients.Client(clientId).SendAsync("SendConversation", new ConversationPreviewDto
            {
                ConversationId = conversationEntityId,
                Name = EntityMapper.GetConversationName(conversation.Members, conversationDto.ReceiverId),
                LastMessage = conversationDto.FirstMessage,
            });
        }

        return conversation.ConEntityToConDto();
    }

    public async Task<ConversationDto> GetConversation(int senderId, int receiverId)
    {
        var conversationEntity = await conversationService.GetConversation(senderId, receiverId);
        return conversationEntity.ConEntityToConDto();
    }

    public async Task<ConversationDto> GetConversation(int conversationId)
    {
        var conversationEntity = await conversationService.GetConversation(conversationId);
        return conversationEntity.ConEntityToConDto();
    }

    public async Task<bool> SendMessage(int conversationId, CreateMessageDto dto)
    {
        await messageService.CreateMessage(dto.SenderId, conversationId, dto.Content);
        IEnumerable<ConversationMember> members = conversationService.GetMembersByConversationId(conversationId).ToList();           

        foreach(var member in members)
        {
            if(member.UserId == dto.SenderId)
            {
                continue;
            }

            ChatAppHub.Users.TryGetValue(member.UserId.ToString(), out string? clientId);

            if(clientId != null)
            {
                hub.Clients.Client(clientId).SendAsync("SendMessage", conversationId, dto);
            }
        }
                        
        return true;
    }

    public async Task<bool> AddMember(int conversationId, AddMemberDto member)
    {
        return await conversationService.AddMember(conversationId, member.UserId);
    }

    public IEnumerable<ConversationPreviewDto> GetConversationsWithCurrentUser(int currentUserId)
    {
        var conversations = conversationService.GetConversationsWithCurrentUser(currentUserId);
        return conversations.ConEntityToConPreviewDtos(currentUserId);
    }
}