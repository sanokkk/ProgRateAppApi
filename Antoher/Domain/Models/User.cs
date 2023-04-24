using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Antoher.Domain.Models
{
    /// <summary>
    /// Модель пользователя,
    /// Используется в качестве базовой для Identity
    /// </summary>
    public class User: IdentityUser
    {
        /// <summary>
        /// Имя (Макс. длина 150 символов)
        /// </summary>
        [Column(TypeName ="varchar(150)")]
        public string FullName { get; set; }

        /// <summary>
        /// Строка для изображения 
        /// </summary>
        public string PictureBase { get; set; }
    }
}
