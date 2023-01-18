using Antoher.DAL.Interfaces;
using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _db;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public CommentController(ICommentRepo db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("{id}")]
        [Route("GetPostComments")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetPostComments([FromQuery]int postId)
        {
            var comments = await _db.SelectPostCommentsAsync(postId);
            return Ok(comments);
        }

        [HttpPost]
        [Authorize]
        [Route("AddComment")]
        public async Task<IActionResult> AddComment([FromQuery]int postId, [FromBody]CommentDto model)
        {
            if (ModelState.IsValid)
            {
                //вытягиваем юзера
                var userId = User.Claims.First(x => x.Type == "UserID").Value;
                var user = _userManager.FindByIdAsync(userId);

                //вытягиваем пост
                var post = _db.GetAsync(postId);

                //проверяем, есть ли такой пост
                if (post == null)
                    return BadRequest();

                //пост есть -> добавляем к нему комментарий
                var comment = new Comment()
                {
                    message = model.message,
                    userId = userId,
                    postId = postId
                };
                await _db.AddAsync(comment);
                return Ok(comment);
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize]
        [Route("DeleteComment")]
        public async Task<IActionResult> DeleteComment([FromQuery] int commentId)
        {
            //вытягиваю юзера 
            var userId = User.Claims.First(x => x.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            //вытягиваю коммент и проверяю, принадлежит ли он текущему юзеру
            var comment = await _db.GetAsync(commentId);
            if (comment.userId != userId)
                return BadRequest();

            //коммент принадлежит юзеру -> удаляем коммент
            await _db.DeleteAsync(comment.commentId);
            return Ok();
        }
    }
}
