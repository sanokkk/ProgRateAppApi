using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.DTO
{
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
    }
}
