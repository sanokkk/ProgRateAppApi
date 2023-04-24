using Antoher.Domain.Models;
using System.Collections.Generic;

namespace Antoher.Domain.DTO
{
    /// <summary>
    /// Дто для получения страницы с постами (для пагинации)
    /// </summary>
    public class PageDto
    {
        public List<Post> page { get; set; }

        public int pages { get; set; }
    }
}
