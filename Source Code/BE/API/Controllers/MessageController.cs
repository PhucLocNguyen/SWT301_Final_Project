using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entity;
using SWP391Project.API.Model.MessageModel;
using SWP391Project.Services.ChatSystem;

namespace SWP391Project.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("{conversationId}/messages")]
        public async Task<IActionResult> GetMessages(int conversationId)
        {
            var messages = await _chatService.GetMessagesAsync(conversationId);
            return Ok(messages);
        }

    }

}
