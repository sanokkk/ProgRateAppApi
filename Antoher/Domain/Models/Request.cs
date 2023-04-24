using System.ComponentModel.DataAnnotations;

namespace Antoher.Domain.Models
{
    /// <summary>
    /// Модель запроса для добавления в друзья
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Айди запроса
        /// </summary>
        [Key]
        public int request_id { get; set; }

        /// <summary>
        /// Айди отправившего запрос
        /// </summary>
        public string issuer_id { get; set; }

        /// <summary>
        /// Айди человека, которому отправили запрос
        /// </summary>
        public string target_id { get; set; }
    }
}
