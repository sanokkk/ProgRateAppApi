using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Antoher.Domain.Models
{
    public class Friend
    {
        [Key]
        public int pair_id { get; set; }
        public string friendOne_id { get; set; }
        
        public string friendTwo_id { get; set; }
    }
}
