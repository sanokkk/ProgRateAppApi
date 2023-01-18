using Antoher.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Antoher.DAL.Interfaces
{
    public interface IPostRepo: IRepo<Post>
    {
        Task<List<Post>> SelectAsync();
    }
}
