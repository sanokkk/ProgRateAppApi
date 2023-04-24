using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.Models
{
    /// <summary>
    /// Модель для комментария
    /// </summary>
    public class Comment
    {
        public int commentId { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Максимальная длина комментария - 500 символов")]
        public string message { get; set; }

        /// <summary>
        /// Айди пользователя, оставившего комментарий
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// Айди поста, к которому добавляется комментарий 
        /// </summary>
        public int postId { get; set; }

        /// <summary>
        /// Строка, которая конвертируется в изображение
        /// </summary>
        public string PictureBase { get; set; }
    }
}
