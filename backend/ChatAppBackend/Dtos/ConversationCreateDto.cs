namespace ChatAppBackend.Dtos
{
    public class ConversationCreateDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string FirstMessage { get; set; }
    }
}
