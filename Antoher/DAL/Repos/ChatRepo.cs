using Antoher.DAL.Interfaces;
using Antoher.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.DAL.Repos
{
    /// <summary>
    /// Репозиторий для взаимодействия с данными из бд для чатов.
    /// Реализиует интерфейс IChatRepo
    /// </summary>
    public class ChatRepo : IChatRepo
    {
        /// <summary>
        /// Конеткст бд для инициализации через DI
        /// </summary>
        private readonly ApplicationDbContext _db;
        /// <summary>
        /// инициализация контекста через DI
        /// </summary>
        /// <param name="db"></param>
        public ChatRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Добавления сообщения в бд
        /// </summary>
        /// <param name="entity">Сообщение</param>
        /// <returns></returns>
        public async Task AddAsync(ChatMessage entity)
        {
            await _db.messages.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        
        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ChatMessage> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Проверка, есть ли пользователь в чате
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task<bool> isUserIn(string userId, string groupName)
        {
            return await _db.messages.AnyAsync(x => x.UserId == userId && x.GroupName == groupName);
        }

        /// <summary>
        /// Возвращает коллекцию сообщений
        /// </summary>
        /// <returns>Коллеция сообщений</returns>
        public async Task<ChatMessage[]> SelectAsync()
        {
            return await _db.messages.ToArrayAsync();
        }

        /// <summary>
        /// Выборка сообщений определенной беседы
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns>Коллеция сообщений</returns>
        public async Task<ChatMessage[]> SelectByGroupAsync(string groupName)
        {
            return await _db.messages.Where(x => x.GroupName == groupName).ToArrayAsync();
        }

        public Task UpdateAsync(ChatMessage entity)
        {
            throw new System.NotImplementedException();
        }


    }
}
