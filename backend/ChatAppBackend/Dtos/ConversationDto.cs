using ChatAppBackend.Entities;
using System.ComponentModel.DataAnnotations;

namespace ChatAppBackend.Dtos
{
    public class ConversationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<MessageDto> Messages { get; set; }
        public IEnumerable<MemberDto> Members { get; set; }
    }
}
