using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.DTO
{
    public class MessageDto
    {
        [Required]
        public string Message { get; set; }
    }
}
