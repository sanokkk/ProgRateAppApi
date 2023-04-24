namespace Antoher.Domain.Models
{
    /// <summary>
    /// Модель для сообщения чата
    /// </summary>
    public class ChatMessage
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public string GroupName { get; set; }

        public string Message { get; set; }
    }
}
