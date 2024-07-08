using ChatApp.DAL;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DOMAIN
{
    public class MessageService
    {
        private readonly ChatAppDbContext _context;

        public MessageService(ChatAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChatMessageDTO>> GetAllChatMessagesAsync(int chatId)
        {
            var messages = await _context.Messages
                .Where(m => m.ChatId == chatId)
                .ToListAsync();

            return messages.Select(ChatMessageDTO.MessageToMessageDTO).ToList();
        }

        public async Task<IEnumerable<ChatMessageDTO>> GetAllUserMessagesAsync(int userId)
        {
            var messages = await _context.Messages
                .Where(m => m.UserId == userId)
                .ToListAsync();

            return messages.Select(ChatMessageDTO.MessageToMessageDTO).ToList();
        }

        public async Task<IEnumerable<ChatMessageDTO>> GetAllChatUserMessagesAsync(int chatId, int userId)
        {
            var messages = await _context.Messages
                .Where(m => m.ChatId == chatId && m.UserId == userId)
                .ToListAsync();

            return messages.Select(ChatMessageDTO.MessageToMessageDTO).ToList();
        }

        public async Task<ChatMessageDTO> AddMessageAsync(CreateMessageDTO request)
        {
            var message = CreateMessageDTO.CreateMessageDTOToMessage(request);

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return ChatMessageDTO.MessageToMessageDTO(message);
        }

        public async Task<ChatMessageDTO> UpdateMessageAsync(int id, UpdateMessageDTO request)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
                throw new CustomException(CustomExceptionType.NotFound, $"Message id {id} wasn't found.");

            UpdateMessageDTO.UpdateMessage(message, request);
            await _context.SaveChangesAsync();

            return ChatMessageDTO.MessageToMessageDTO(message);
        }

        public async Task DeleteMessageAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
                throw new CustomException(CustomExceptionType.NotFound, $"Message id {id} wasn't found.");

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }
}
