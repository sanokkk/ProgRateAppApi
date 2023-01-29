using Antoher.Domain.Models;
using System.Collections.Generic;

namespace Antoher.Domain.DTO
{
    public class PageDto
    {
        public List<Post> page { get; set; }

        public int pages { get; set; }
    }
}
