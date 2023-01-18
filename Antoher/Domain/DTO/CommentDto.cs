using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.DTO
{
    public class CommentDto
    {
        [Required]
        [MaxLength(500, ErrorMessage ="Максимальная длина комментария - 500 символов")]
        public string message { get; set; }
    }
}
