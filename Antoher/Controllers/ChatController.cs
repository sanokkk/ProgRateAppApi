﻿using Antoher.DAL.Interfaces;
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
    [Route("api/[controller]")]
    [ApiController]

    public class ChatController : ControllerBase
    {
        private readonly List<string> _user;
        private readonly List<string> _group;
        private readonly IChatRepo _chatRepo;


        public ChatController(IChatRepo chatRepo)
        {
            _group = new List<string>() { "backend", "frontend", "devops" };
            _user = new List<string>() { "Alex", "Sanya", "Amir", "Sabina" };
            _chatRepo = chatRepo;
        }

        [HttpGet]
        [Route("groups")]
        public async Task<IActionResult> GetGroups()
        {
            return Ok(_group);
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(_user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string group)
        {
            var messages = await _chatRepo.SelectByGroupAsync(group);
            return Ok(messages);
        }

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
