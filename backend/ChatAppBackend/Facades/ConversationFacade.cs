using ChatAppBackend.Dtos;
using ChatAppBackend.Entities;
using ChatAppBackend.Exceptions;
using ChatAppBackend.Hubs;
using ChatAppBackend.Mappers;
using ChatAppBackend.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppBackend.Facades
{
    public class ConversationFacade(ConversationService conversationService, MessageService messageService, IHubContext<ChatAppHub> hub)
    {
        public async Task<ConversationDto> CreateConversation(ConversationCreateDto conversationDto)
        {
            int conversationEntityId = await conversationService.CreateConversation(conversationDto.ReceiverId, conversationDto.SenderId);
            await messageService.CreateMessage(conversationDto.SenderId, conversationEntityId, conversationDto.FirstMessage);

            ConversationEntity entity = await conversationService.GetConversation(conversationEntityId);
            return entity.ConEntityToConDto();
        }

        public async Task<ConversationDto> GetConversation(int senderId, int receiverId)
        {
            ConversationEntity conversationEntity = await conversationService.GetConversation(senderId, receiverId);
            return conversationEntity.ConEntityToConDto();
        }

        public async Task<ConversationDto> GetConversation(int conversationId)
        {
            ConversationEntity conversationEntity = await conversationService.GetConversation(conversationId);
            return conversationEntity.ConEntityToConDto();
        }

        public async Task<bool> SendMessage(int conversationId, CreateMessageDto dto)
        {
            await messageService.CreateMessage(dto.SenderId, conversationId, dto.Content);

            if (hub.Clients == null)
            {
                return false;
            }

            IEnumerable<ConversationMember> members = conversationService.GetMembersByConversationId(conversationId).ToList();           

            foreach(ConversationMember member in members)
            {
                if(member.UserId == dto.SenderId)
                {
                    continue;
                }

                ChatAppHub.Users.TryGetValue(member.UserId.ToString(), out string clientId);

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

        public IEnumerable<ConversationPreviewDto> GetALlConversations()
        {
            IQueryable<ConversationEntity> conversations = conversationService.GetAllConversations();
            return conversations.ConEntityToConPreviewDtos();
        }

        public IEnumerable<ConversationPreviewDto> GetConversationsByMemberId(int memberId)
        {
            var conversations = conversationService.GetConversationsByMemberId(memberId);
            return conversations.ConEntityToConPreviewDtos();
        }
    }
 }
