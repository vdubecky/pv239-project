namespace ChatAppBackend.Dtos
{
    public class CreateMessageDto
    {
        public int SenderId { get; set; }
        public string Content { get; set; }
    }
}
