namespace Antoher.Domain.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public string GroupName { get; set; }

        public string Message { get; set; }
    }
}
