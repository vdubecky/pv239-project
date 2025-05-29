using ChatAppBackend.Dtos;
using ChatAppBackend.Facades;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppBackend.Controllers
{
    [ApiController]
    [Route("api/v1/conversations")]
    public class ConversationController(ConversationFacade conversationFacade) : ControllerBase
    {
        [HttpPost]
        public async Task<ConversationDto> CreateConversation(ConversationCreateDto conversationDto)
        {
            return await conversationFacade.CreateConversation(conversationDto);
        }

        [HttpGet("by-members")]
        public async Task<ConversationDto> GetConversationByMembers(int senderId, int receiverId)
        {
            return await conversationFacade.GetConversation(senderId, receiverId);
        }

        [HttpGet("{conversationId}")]
        public async Task<ConversationDto> GetConversationById(int conversationId)
        {
            return await conversationFacade.GetConversation(conversationId);
        }

        [HttpGet("/all")]
        public IEnumerable<ConversationPreviewDto> GetConversationsPreviews()
        {
            return conversationFacade.GetALlConversations();
        }

        [HttpGet("/all/{memberId}")]
        public IEnumerable<ConversationPreviewDto> GetConversationsPreviewsByMemberId(int memberId)
        {
            return conversationFacade.GetConversationsByMemberId(memberId);
        }

        [HttpPost("{id}/messages")]
        public async Task<bool> SendMessage(int id, CreateMessageDto sendMessageDto)
        {
            return await conversationFacade.SendMessage(id, sendMessageDto);
        }

        [HttpPost("{id}/memebers")]
        public async Task<bool> AddMember(int id, AddMemberDto member)
        {
            return await conversationFacade.AddMember(id, member);
        }
    }
}
