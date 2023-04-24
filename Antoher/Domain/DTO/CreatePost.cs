using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.DTO
{
    /// <summary>
    /// Дто для создания поста
    /// </summary>
    public class CreatePost
    {
        [Required]
        public string title { get; set; }

        [Required]
        public string plot { get; set; }

        public string PictureBase { get; set; }
    }
}
