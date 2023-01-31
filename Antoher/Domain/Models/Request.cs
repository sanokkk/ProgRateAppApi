using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.Models
{
    public class Request
    {
        [Key]
        public int request_id { get; set; }

        public string issuer_id { get; set; }

        public string target_id { get; set; }
    }
}
