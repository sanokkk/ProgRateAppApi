using Antoher.DAL.Interfaces;
using Antoher.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.DAL.Repos
{
    /// <summary>
    /// Класс репозитория для взаимодействия с контекстом БД
    /// </summary>
    public class PostRepo : IPostRepo
    {
        /// <summary>
        /// Конеткст бд, инициализируется через DI в конструкторе
        /// </summary>
        private readonly ApplicationDbContext _db;
        /// <summary>
        /// Инициализация контекста
        /// </summary>
        /// <param name="db">конекст бд</param>
        public PostRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Добавление поста
        /// </summary>
        /// <param name="entity">Сущность поста</param>
        public async Task AddAsync(Post entity)
        {
            await _db.posts.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление поста по айди
        /// </summary>
        /// <param name="id">Айди поста</param>
        public async Task DeleteAsync(int id)
        {
            var post = await _db.posts.FirstOrDefaultAsync(x => x.postId == id);
            _db.posts.Remove(post);
            await _db.SaveChangesAsync();
        }


        /// <summary>
        /// Получение поста по айди
        /// </summary>
        /// <param name="id">Айди поста</param>
        /// <returns>Объект поста</returns>
        public async Task<Post> GetAsync(int id)
        {
            var post = await _db.posts.FirstOrDefaultAsync(x => x.postId == id);
            return post;
        }

        /// <summary>
        /// Получение всех постов
        /// </summary>
        /// <returns>Список постов</returns>
        public Task<List<Post>> SelectAsync()
        {
            var posts = _db.posts.ToListAsync();
            return posts;
        }

        /// <summary>
        /// Поиск поста по заголовку
        /// </summary>
        /// <param name="title">Заголовок</param>
        /// <returns>Релевантный список постов</returns>
        public async Task<List<Post>> SelectByTitleAsync(string title)
        {
            var posts = await _db.posts.Where(x => x.title.ToLower().Contains(title)).ToListAsync();
            return posts;
        }

        /// <summary>
        /// Получение поста по айди
        /// </summary>
        /// <param name="id">Айди поста</param>
        /// <returns>Объект поста</returns>
        public async Task<Post> SelectPostByIdAsync(int id)
        {
            var post = await _db.posts.FirstOrDefaultAsync(x => x.postId == id);
            return post;
        }

        /// <summary>
        /// Выборка постов определенного пользователя
        /// </summary>
        /// <param name="userId">Айди пользователя</param>
        /// <returns>Релевантный список постов</returns>
        public async Task<List<Post>> SelectUserPostsAsync(string userId)
        {
            var posts = await _db.posts.Where(x => x.userId == userId).ToListAsync();
            return posts;
        }

        public async Task UpdateAsync(Post entity)
        {
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Выбор поста юзера по айди
        /// </summary>
        /// <param name="userId">Айди пользователя</param>
        /// <param name="postId">Айди поста</param>
        /// <returns>Объект поста</returns>
        public async Task<Post> SelectByUserAndIdAsync(string userId, int postId)
        {
            var post = await _db.posts.FirstOrDefaultAsync(x => x.userId == userId && x.postId == postId);
            return post;
        }
    }
}
