using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ChatAppBackend.Entities
{
    [Table("Conversations")]
    public class ConversationEntity
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public ICollection<MessageEntity> Messages { get; set; }
        public ICollection<ConversationMember> Members { get; set; }
    }
}
