using System.Threading.Tasks;

namespace Antoher.DAL.Interfaces
{
    /// <summary>
    /// Базовый интерфейс для репозиториев с базовыми методами
    /// </summary>
    /// <typeparam name="T">Дженерик параметр</typeparam>
    public interface IRepo<T>
    {
        Task<T> GetAsync(int id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);
    }
}
