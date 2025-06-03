using ChatAppBackend.Dtos;
using ChatAppBackend.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppBackend.Controllers;

[Route("api/v1/conversations")]
public class ConversationController(ConversationFacade conversationFacade) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ConversationDto>> CreateConversation([FromBody] ConversationCreateDto conversationDto)
    {
        return await conversationFacade.CreateConversation(conversationDto);
    }

    [Authorize]
    [HttpGet("by-members")]
    public async Task<ActionResult<ConversationDto>> GetConversationByMembers(int senderId, int receiverId)
    {
        return await conversationFacade.GetConversation(senderId, receiverId);
    }

    [Authorize]
    [HttpGet("{conversationId}")] // I'd suggest adding constraints to the route to ensure it only accepts integers: {conversationId:int}. They can be used for some other types, such as guid, as well.
    public async Task<ActionResult<ConversationDto>> GetConversationById(int conversationId)
    {
        return await conversationFacade.GetConversation(conversationId);
    }

    [Authorize]
    [HttpGet("/all/{currentUserId}")]
    public IEnumerable<ConversationPreviewDto> GetConversationsWithCurrentUser(int currentUserId)
    {
        return conversationFacade.GetConversationsWithCurrentUser(currentUserId);
    }

    [Authorize]
    [HttpPost("{id}/messages")]
    public async Task<ActionResult<bool>> SendMessage(int id, [FromBody] CreateMessageDto sendMessageDto)
    {
        return await conversationFacade.SendMessage(id, sendMessageDto);
    }

    [Authorize]
    [HttpPost("{id}/memebers")]
    public async Task<ActionResult<bool>> AddMember(int id, [FromBody] AddMemberDto member)
    {
        return await conversationFacade.AddMember(id, member);
    }
}