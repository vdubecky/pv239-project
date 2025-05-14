using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppBackend.Entities
{
    [Table("Messages")]
    public class MessageEntity
    {
        public int Id { get; set; }
       
        public string Content { get; set; }

        public int SenderId { get; set; }
        public UserEntity SenderEntity { get; set; }

        public int ConversationId { get; set; }
        public ConversationEntity ConversationEntity { get; set; }

        public DateTime SentAt { get; set; }
    }
}
