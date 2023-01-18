using Antoher.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Antoher.DAL.Interfaces
{
    public interface ICommentRepo: IRepo<Comment>
    {
        Task<List<Comment>> SelectPostCommentsAsync(int postId);
    }
}
