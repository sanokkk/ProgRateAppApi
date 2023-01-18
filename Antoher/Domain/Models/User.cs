using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Antoher.Domain.Models
{
    public class User: IdentityUser
    {
        [Column(TypeName ="varchar(150)")]
        public string FullName { get; set; }
    }
}
