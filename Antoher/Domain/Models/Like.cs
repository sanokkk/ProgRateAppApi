namespace Antoher.Domain.Models
{
    /// <summary>
    /// Модель для хранения лайка к посту
    /// </summary>
    public class Like
    {
        public int likeId { get; set; }

        public string userId { get; set; }

        public int postId { get; set; }
    }
}
