using System.Threading.Tasks;

namespace Antoher.DAL.Interfaces
{
    public interface IRepo<T>
    {
        Task<T> GetAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);
    }
}
