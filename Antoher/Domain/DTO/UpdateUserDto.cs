using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.DTO
{
    /// <summary>
    /// Дто для обновления данных пользователя (имя, ник, почта, аватар)
    /// </summary>
    public class UpdateUserDto
    {
        [Required]
        [MinLength(5, ErrorMessage ="Минимальная длина ника - 5 символов")]
        [MaxLength(30, ErrorMessage ="Максимальная длина ника - 30 символов")]
        public string UserName { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Минимальная длина имени - 10 символов")]
        [MaxLength(70, ErrorMessage = "Максимальная длина ника - 70 символов")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PictureBase { get; set; }
    }
}
