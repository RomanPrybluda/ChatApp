using ChatApp.DOMAIN;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Controllers
{

    [Route("chat")]

    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllChatsAsync()
        {
            var chats = await _chatService.GetAllChatsAsync();
            return Ok(chats);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetChat([Required] int id)
        {
            var chat = await _chatService.GetChatByIdAsync(id);
            return Ok(chat);
        }

        [HttpPost]
        public async Task<ActionResult> CreateChatAsync([Required][FromBody] CreateChatDTO request, int userId)
        {
            var chat = await _chatService.CreateChatAsync(request, userId);
            return Ok(chat);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateChat([Required] int id, [FromBody] UpdateChatDTO request)
        {
            var chat = await _chatService.UpdateChatAsync(id, request);
            return Ok(chat);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChatAsync(int id, int userId)
        {
            await _chatService.DeleteChatAsync(id, userId);
            return NoContent();
        }
    }
}
