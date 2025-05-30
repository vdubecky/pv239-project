using ChatAppBackend.Dtos;
using ChatAppBackend.Facades;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppBackend.Controllers;

[Route("api/v1/conversations")]
public class ConversationController(ConversationFacade conversationFacade) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ConversationDto>> CreateConversation([FromBody] ConversationCreateDto conversationDto)
    {
        return await conversationFacade.CreateConversation(conversationDto);
    }

    [HttpGet("by-members")]
    public async Task<ActionResult<ConversationDto>> GetConversationByMembers(int senderId, int receiverId)
    {
        return await conversationFacade.GetConversation(senderId, receiverId);
    }

    [HttpGet("{conversationId}")]
    public async Task<ActionResult<ConversationDto>> GetConversationById(int conversationId)
    {
        return await conversationFacade.GetConversation(conversationId);
    }

    [HttpGet("/all/{currentUserId}")]
    public IEnumerable<ConversationPreviewDto> GetConversationsWithCurrentUser(int currentUserId)
    {
        return conversationFacade.GetConversationsWithCurrentUser(currentUserId);
    }

    [HttpPost("{id}/messages")]
    public async Task<ActionResult<bool>> SendMessage(int id, [FromBody] CreateMessageDto sendMessageDto)
    {
        return await conversationFacade.SendMessage(id, sendMessageDto);
    }

    [HttpPost("{id}/memebers")]
    public async Task<ActionResult<bool>> AddMember(int id, [FromBody] AddMemberDto member)
    {
        return await conversationFacade.AddMember(id, member);
    }
}