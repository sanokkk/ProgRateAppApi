using Antoher.DAL.Interfaces;
using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using Antoher.Hubs;
using Antoher.Hubs.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.Controllers
{
    /// <summary>
    /// Контроллер для чатов
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class ChatController : ControllerBase
    {
        private readonly IChatRepo _chatRepo;

        /// <summary>
        /// Конструктор для инициализации репозитория через DI
        /// </summary>
        /// <param name="chatRepo">интерфейс IChatRepo</param>
        public ChatController(IChatRepo chatRepo)
        {
            _chatRepo = chatRepo;
        }

        
        /// <summary>
        /// Метод для получения всех сообщений группы из бд
        /// </summary>
        /// <param name="group">Название группы</param>
        /// <returns>200 (Ok) - Список релевантных сообщений</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string group)
        {
            var messages = await _chatRepo.SelectByGroupAsync(group);
            return Ok(messages);
        }


        /// <summary>
        /// Метод для отправки сообщений в группу
        /// </summary>
        /// <param name="message">Дто сообщения</param>
        /// <param name="group">Название группы</param>
        /// <returns>200 (Ok)</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody]MessageDto message, [FromQuery]string group)
        {
            var userId = User.Claims.First(x => x.Type == "UserID").Value;
            var chatMsg = new ChatMessage() { GroupName = group, Message = message.Message, UserId = userId };
            await _chatRepo.AddAsync(chatMsg);
            return Ok(_chatRepo);

        }
















    }
}
