using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.DTO
{
    /// <summary>
    /// Дто для регистрации
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Имя (обязательное поле, длина: 10 - 80)
        /// </summary>
        [Required]
        [MinLength(10, ErrorMessage ="Минимальная длина ФИО - 10 символов")]
        [MaxLength(80, ErrorMessage ="Максимальная длина ФИО - 80 символов")]
        public string FullName { get; set; }


        /// <summary>
        /// Почта (обяз. поле, длина: 8 - 30)
        /// </summary>
        [EmailAddress]
        [Required]
        public string Email { get; set; }


        /// <summary>
        /// Пароль (обяз. поле, длина: 8- 30)
        /// </summary>
        [MinLength(8, ErrorMessage ="Минимальная длина пароля - 8 символов")]
        [MaxLength(30, ErrorMessage ="Максимальная длина пароля - 30 символов")]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Ник (обяз. поле, длина: 5-30)
        /// </summary>
        [MinLength(5, ErrorMessage ="Минимальная длина никнейма - 5 символов")]
        [MaxLength(30, ErrorMessage ="Максимальная длина никнейма - 30 символов")]
        [Required]
        public string UserName { get; set; }

    }
}
