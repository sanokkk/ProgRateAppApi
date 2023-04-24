using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.DTO
{
    /// <summary>
    /// Дто для комментария
    /// </summary>
    public class CommentDto
    {
        [Required]
        [MaxLength(500, ErrorMessage ="Максимальная длина комментария - 500 символов")]
        public string message { get; set; }

        public string PictureBase { get; set; }
    }
}
