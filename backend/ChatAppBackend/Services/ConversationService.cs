using ChatAppBackend.Entities;
using ChatAppBackend.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ChatAppBackend.Services;

public class ConversationService(ChatAppDbContext dbContext)
{
    public async Task<int> CreateConversation(int receiverId, int senderId)
    {
        ConversationEntity conversationEntity = new()
        {
            Name = "New conversation"
        };

        dbContext.Conversations.Add(conversationEntity);
        await dbContext.SaveChangesAsync();

        ConversationMember sender = new()
        {
            UserId = senderId,
            ConversationId = conversationEntity.Id,
        };

        ConversationMember receiver = new()
        {
            UserId = receiverId,
            ConversationId = conversationEntity.Id,
        };

        dbContext.ConversationMembers.Add(sender);
        dbContext.ConversationMembers.Add(receiver);
        await dbContext.SaveChangesAsync();
            
        return conversationEntity.Id;
    }

    public async Task<bool> AddMember(int conversationId, int userId)
    {
        ConversationMember conversationMember = new ConversationMember
        {
            UserId = userId,
            ConversationId = conversationId,
        };

        dbContext.ConversationMembers.Add(conversationMember);
        return await dbContext.SaveChangesAsync() > 0;
    }

    // There's no need for this method to be an async Task, as it does not perform any asynchronous operations.
    public async Task<ConversationEntity> GetConversation(int senderId, int receiverId)
    {
        var conversations = dbContext.Conversations
            .Include(c => c.Members).ThenInclude(m => m.UserEntity)
            .Where(c => c.Members.Count == 2)
            .Include(c => c.Messages)
            .Where(c => c.Members.Any(m => m.UserId == senderId) && c.Members.Any(m => m.UserId == receiverId));

        if (!conversations.Any())
        {
            // Exceptions are expensive resource-wise and a missing conversation is a common and expected case, so you might want to return null instead of throwing an exception.
            // It also results into errors in the logs and makes it harder to notice real issues.
            throw new EntityNotFoundException($"Conversation between {senderId} and {receiverId} not found");
        }

        return conversations.First();
    }
    
    public async Task<ConversationEntity> GetConversation(int conversationId)
    {
        var conversations = dbContext.Conversations
            .Include(c => c.Messages)
            .Include(c => c.Members).ThenInclude(m => m.UserEntity)
            .Where(c => c.Id == conversationId);

        if (!conversations.Any())
        {
            throw new EntityNotFoundException($"Conversation with ID {conversationId} not found");
        }

        return conversations.First();
    }

    public IQueryable<ConversationEntity> GetConversationsWithCurrentUser(int currentUserId)
    {
        return dbContext.Conversations
            .Include(c => c.Members).ThenInclude(m => m.UserEntity)
            .Where(c => c.Members.Any(m => m.UserId == currentUserId))
            .OrderByDescending(c => c.LastMessage.SentAt);
    }

    public IQueryable<ConversationMember> GetMembersByConversationId(int conversationId)
    {
        return dbContext.ConversationMembers
            .Where(m => m.ConversationId == conversationId);
    }
}
