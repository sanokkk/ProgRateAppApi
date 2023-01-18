using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.DTO
{
    public class RegisterDto
    {
        [Required]
        [MinLength(10, ErrorMessage ="Минимальная длина ФИО - 10 символов")]
        [MaxLength(80, ErrorMessage ="Максимальная длина ФИО - 80 символов")]
        public string FullName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [MinLength(8, ErrorMessage ="Минимальная длина пароля - 8 символов")]
        [MaxLength(30, ErrorMessage ="Максимальная длина пароля - 30 символов")]
        [Required]
        public string Password { get; set; }

        [MinLength(5, ErrorMessage ="Минимальная длина никнейма - 5 символов")]
        [MaxLength(30, ErrorMessage ="Максимальная длина никнейма - 30 символов")]
        [Required]
        public string UserName { get; set; }

    }
}
