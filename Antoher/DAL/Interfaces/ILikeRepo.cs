using Antoher.Domain.Models;
using System.Threading.Tasks;

namespace Antoher.DAL.Interfaces
{
    public interface ILikeRepo: IRepo<Like>
    {
        Task<bool> IsLikedAsync(int postId, string userId);
    }
}
