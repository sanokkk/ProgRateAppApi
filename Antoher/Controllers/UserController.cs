using Antoher.DAL;
using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Antoher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _db;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext db, IPasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _passwordHasher = passwordHasher;
        }

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
                    UserName = user.UserName
                };
                return Ok(userDto);
            }
        }

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
                userId = user.Id
            };
            return Ok(getUser);
        }


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

                //var password = new string(Encoding.UTF8.GetBytes(user.PasswordHash).toCharArray);

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


        #region Логаут не работает
        [HttpPost]
        [Route("Logout")]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            var userId = User.Claims.First(x => x.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.UpdateSecurityStampAsync(user);
            await _userManager.RemoveAuthenticationTokenAsync(user, "JWT", "JWT Token");
            
            
            //HttpContext.Response.Cookies.Delete();
            return Ok();
        }
        #endregion
    }
}
