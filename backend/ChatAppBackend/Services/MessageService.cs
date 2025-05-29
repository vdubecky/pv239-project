using ChatAppBackend.Entities;

namespace ChatAppBackend.Services;

public class MessageService(ChatAppDbContext dbContext)
{
    public async Task<MessageEntity> CreateMessage(int senderId, int conversationId, string message)
    {
        var messageEntity = new MessageEntity
        {
            SenderId = senderId,
            ConversationId = conversationId,
            Content = message,
            SentAt = DateTime.UtcNow
        };

        dbContext.Messages.Add(messageEntity);
        await dbContext.SaveChangesAsync();
        
        var conversation = dbContext.Conversations
            .FirstOrDefault(c => c.Id == conversationId);

        if (conversation != null)
        {
            conversation.LastMessageId = messageEntity.Id;
            await dbContext.SaveChangesAsync();
        }

        return messageEntity;
    }
}