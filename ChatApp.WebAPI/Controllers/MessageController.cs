using ChatApp.DOMAIN;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.WebAPI
{
    [ApiController]
    [Produces("application/json")]
    [Route("message")]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("{chatId}")]
        public async Task<ActionResult> GetAllChatMessagesAsync([Required] int chatId)
        {
            var messages = await _messageService.GetAllChatMessagesAsync(chatId);
            return Ok(messages);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetAllUserMessagesAsync([Required] int userId)
        {
            var messages = await _messageService.GetAllUserMessagesAsync(userId);
            return Ok(messages);
        }

        [HttpGet("{chatId}/{userId}")]
        public async Task<ActionResult> GetAllChatUserMessagesAsync([Required] int chatId, [Required] int userId)
        {
            var messages = await _messageService.GetAllChatUserMessagesAsync(chatId, userId);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult> AddMessageAsync([Required][FromBody] CreateMessageDTO request)
        {
            var message = await _messageService.AddMessageAsync(request);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMessageAsync([Required] int id, [FromBody] UpdateMessageDTO request)
        {
            var message = await _messageService.UpdateMessageAsync(id, request);
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessageAsync([Required] int id)
        {
            await _messageService.DeleteMessageAsync(id);
            return NoContent();
        }
    }
}
