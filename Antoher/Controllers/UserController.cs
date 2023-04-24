using Antoher.DAL;
using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Antoher.Controllers
{
    /// <summary>
    /// Контроллер для вазимодействия с сущностью пользователя
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;
        /// <summary>
        /// Конструктор для инициализации полей через DI
        /// </summary>
        /// <param name="userManager">Для Identity (содержит методы для сущности пользователя)</param>
        /// <param name="db">Контекст БД</param>
        public UserController(UserManager<User> userManager,  ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }


        /// <summary>
        /// Метод регистрации
        /// </summary>
        /// <param name="model">Дто для регистрации из body</param>
        /// <returns>В случае успеха - 200 (Ok), иначе 400 (BadRequest)</returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName
                };
                
                var result = await _userManager.CreateAsync(user, model.Password);
                return Ok(result);
                
            }
            return BadRequest();
        }

        /// <summary>
        /// Получение пользователя по айди
        /// </summary>
        /// <param name="Id">айди пользователя</param>
        /// <returns>В случае успеха - 200 (Ok), иначе 400 (BadRequest)</returns>
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user is null)
            {
                return BadRequest();
            }
            else
            {
                var userDto = new GetUserDto()
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    PictureBase = user.PictureBase
                };
                return Ok(userDto);
            }
        }

        /// <summary>
        /// HTTP (GET) метод для получения данных о текущем авторизованном пользователе
        /// </summary>
        /// <returns>В случае успеха - 200 (Ok), иначе 401 (Unauthorized)</returns>
        [HttpGet]
        [Authorize]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var userId = User.Claims.First(x => x.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            

            var getUser = new GetUserDto()
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                userId = user.Id,
                PictureBase = user.PictureBase
            };
            return Ok(getUser);
        }

        /// <summary>
        /// Метод для авторизации
        /// Генерируется jwt токен, создаются claims
        /// </summary>
        /// <param name="model">Дто для логина</param>
        /// <returns>В случае успеха - 200 (Ok) с jwt токеном, иначе 400 (BadRequest)</returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user,model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(12),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            return BadRequest("UserName or password is incorrect.");
        }


        /// <summary>
        /// Метод для удаления аккаунта у текущего пользователя
        /// </summary>
        /// <returns>В случае успеха - 200 (Ok), иначе 401 (Unauthorized)</returns>
        [HttpPost]
        [Authorize]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser()
        {
            string user = User.Claims.First(x => x.Type == "UserID").Value;
            var userEntity = await _userManager.FindByIdAsync(user);
            await _userManager.DeleteAsync(userEntity);
            await _db.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Метод для обновления данных пользователя
        /// </summary>
        /// <param name="model">Дто юзера</param>
        /// <returns>В случае успеха - 200 (Ok), иначе 401 (Unauthorized) или 400 (BadRequest), если не проходит валидация</returns>
        [HttpPost]
        [Authorize]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserDto model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Claims.First(x => x.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                user.UserName = model.UserName;
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.PictureBase = model.PictureBase;

                await _userManager.UpdateAsync(user);
                await _db.SaveChangesAsync();

                return Ok(user);
            }
            return BadRequest();
        }

        #region Изменение пароля (добавить потом)
        [HttpPost]
        [Authorize]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Claims.First(x => x.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);

                

                var pwd = await _userManager.CheckPasswordAsync(user, model.OldPassword);

                

                if (!pwd)
                {
                    return BadRequest("Старый пароль неверный");
                }

                else
                {
                    await _userManager.ChangePasswordAsync(user, user.PasswordHash, model.Password);
                    await _db.SaveChangesAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }
#endregion
    }
}
