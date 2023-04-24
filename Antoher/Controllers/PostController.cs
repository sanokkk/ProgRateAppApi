using Antoher.DAL;
using Antoher.DAL.Interfaces;
using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using Antoher.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.Controllers
{
    /// <summary>
    /// Контроллер для взаимодействия с постами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IPostRepo _post;

        /// <summary>
        /// Контруктор для получения сервисов через DI
        /// </summary>
        /// <param name="userManager">Взаимодействие с сущностью юзера</param>
        /// <param name="db">Контекст бд</param>
        /// <param name="post">Репозиторий для юзеров</param>
        public PostController(UserManager<User> userManager, ApplicationDbContext db,
            IPostRepo post)
        {
            _userManager = userManager;
            _db = db;
            _post = post;
        }

        /// <summary>
        /// Метод для получения всех постов (постранично)
        /// </summary>
        /// <param name="pageNum">номер страницы (по умолчанию 0)</param>
        /// <returns>В случае успеха - 200 (Ok) со списком постов</returns>
        [Route("SelectAll")]
        [HttpGet]
        public async Task<IActionResult> SelectAll(int pageNum = 1)
        {
            var response = await _db.posts.ToListAsync();
            if (response.Count == 0)
                return Ok(response);
            
            response.Reverse();

            int divider = 20;
            
            var pages = response.Chunk(divider).ToList();
            var resultPage = pages[pageNum - 1].ToList();

            var pageDto = new PageDto()
            {
                page = resultPage,
                pages = pages.Count
            };

            return Ok(pageDto);
        }

        /// <summary>
        /// Метод для получения поста по заголовку
        /// </summary>
        /// <param name="querry">Заголовок</param>
        /// <returns>В случае успеха - 200 (Ok) с объектом поста</returns>
        [HttpGet]
        [Route("SelectByTitle")]
        public async Task<IActionResult> SelectByTitle([FromQuery] string querry)
        {
            var posts = await _post.SelectByTitleAsync(querry);
            return Ok(posts);
        }

        /// <summary>
        /// Метод для получения поста по айди
        /// </summary>
        /// <param name="postId">Айди поста</param>
        /// <returns>В случае успеха - 200 (Ok) с объектом поста, иначе 400 (BadRequest)</returns>
        [HttpGet]
        [Route("SelectById")]
        public async Task<IActionResult> SelectPostById([FromQuery] int postId)
        {
            var post = await _post.SelectPostByIdAsync(postId);
            if (post is null)
                return BadRequest();
            else
                return Ok(post);
        }

        /// <summary>
        /// Метод для получения постов текущего пользователя
        /// </summary>
        /// <returns>В случае успеха - 200 (Ok), иначе 401 (Unauthorized)</returns>
        [Route("Select")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Select()
        {
            string userId = User.Claims.First(x => x.Type == "UserID").Value;

            var posts = await _post.SelectAsync();
            var userPosts = posts.Where(x => x.userId == userId);
            return Ok(userPosts);
        }

        /// <summary>
        /// Метод для создания поста
        /// </summary>
        /// <param name="model">Дто поста из body</param>
        /// <returns>В случае успеха - 200 (Ok), иначе 401 (Unauthorized)</returns>
        [Route("CreatePost")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePost model)
        {
            string IdUser = User.Claims.First(x => x.Type == "UserID").Value;
            var post = new Post()
            {
                userId = IdUser,
                title = model.title,
                plot = model.plot,
                likes = 0,
                PictureBase = model.PictureBase
            };

            await _post.AddAsync(post);
            return Ok(post);
        }

        /// <summary>
        /// Метод для удаления поста
        /// </summary>
        /// <param name="postId">Айди поста</param>
        /// <returns>В случае успеха - 200 (Ok), иначе 204 (NoContent)</returns>
        [Route("DeletePost")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeletePost([FromQuery] int postId)
        {
            var userId = User.Claims.First(x => x.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            var posts = await _post.SelectAsync();
            var post = posts.FirstOrDefault(x => x.postId == postId && x.userId == user.Id);

            //если пост существует -> удаляем
            if (post != null)
            {
                await _post.DeleteAsync(post.postId);
                return Ok();
            }
            return NoContent();
        }

        /// <summary>
        /// Метод для получения постов пользователя (на его странице)
        /// </summary>
        /// <param name="userId">Айди пользователя</param>
        /// <returns>В случае успеха - 200 (Ok)</returns>
        [HttpGet]
        [Route("SelectUserPosts")]
        public async Task<IActionResult> GetUserPost(string userId)
        {
            var posts = await _post.SelectUserPostsAsync(userId);
            return Ok(posts);
        }

        /// <summary>
        /// Метод для обновления информации в посте
        /// </summary>
        /// <param name="newPost">Дто поста с новой информацией</param>
        /// <param name="postId">айди поста</param>
        /// <returns>В случае успеха - 200 (Ok), иначе 401 (Unauthorized)</returns>
        [HttpPost]
        [Authorize]
        [Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody] CreatePost newPost, [FromQuery] int postId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var userId = User.Claims.First(x => x.Type == "UserID").Value;
            var post = await _post.SelectByUserAndIdAsync(userId, postId);
            if (post == null)
                return BadRequest();
            post.title = newPost.title;
            post.plot = newPost.plot;
            post.PictureBase = newPost.PictureBase;
            await _post.UpdateAsync(post);
            return Ok(post);
            
        }


    }
}
