using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppBackend.Entities
{
    [Table("ConversationMembers")]
    public class ConversationMember
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserEntity UserEntity { get; set; }

        public int ConversationId { get; set; }
        public ConversationEntity ConversationEntity { get; set; }
    }
}
