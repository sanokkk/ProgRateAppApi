using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.DTO
{
    public class UpdatePasswordDto
    {
        [Required]
        [MinLength(8, ErrorMessage ="Минимальная длина пароля - 8 символов")]
        public string Password { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage ="Пароли должны совпадать")]
        public string ConfirmPassword { get; set; }
    }
}
