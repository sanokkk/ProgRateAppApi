using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.Models
{
    public class Comment
    {
        public int commentId { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Максимальная длина комментария - 500 символов")]
        public string message { get; set; }

        public string userId { get; set; }

        public int postId { get; set; }
    }
}
