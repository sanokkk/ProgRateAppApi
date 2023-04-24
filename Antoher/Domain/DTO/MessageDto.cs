using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.DTO
{
    /// <summary>
    /// Дто для сообщения
    /// </summary>
    public class MessageDto
    {
        [Required]
        public string Message { get; set; }
    }
}
