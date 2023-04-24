namespace Antoher.Domain.Models
{
    /// <summary>
    /// Модель поста
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Айди поста
        /// </summary>
        public int postId { get; set; }

        /// <summary>
        /// Айди пользователя, создающего пост
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string plot { get; set; }

        /// <summary>
        /// Количество лайков
        /// </summary>
        public int likes { get; set; }

        /// <summary>
        /// Строка, которая конвертируется в изображение
        /// </summary>
        public string PictureBase { get; set; }
    }
}
