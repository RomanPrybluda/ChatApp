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

        public async Task<IEnumerable<MessageDTO>> GetAllChatMessagesAsync(int chatId)
        {
            var messages = await _context.Messages
                .Where(m => m.ChatId == chatId)
                .ToListAsync();

            return messages.Select(MessageDTO.MessageToMessageDTO).ToList();
        }

        public async Task<IEnumerable<MessageDTO>> GetAllUserMessagesAsync(int userId)
        {
            var messages = await _context.Messages
                .Where(m => m.UserId == userId)
                .ToListAsync();

            return messages.Select(MessageDTO.MessageToMessageDTO).ToList();
        }

        public async Task<IEnumerable<MessageDTO>> GetAllChatUserMessagesAsync(int chatId, int userId)
        {
            var messages = await _context.Messages
                .Where(m => m.ChatId == chatId && m.UserId == userId)
                .ToListAsync();

            return messages.Select(MessageDTO.MessageToMessageDTO).ToList();
        }

        public async Task<MessageDTO> AddMessageAsync(CreateMessageDTO request)
        {

            var userById = await _context.Users.FirstOrDefaultAsync(c => c.UserId == request.UserId);

            if (userById == null)
                throw new CustomException(CustomExceptionType.NotFound, $"User with id:{request.UserId} wasn't found.");

            var chatById = await _context.Chats.FirstOrDefaultAsync(c => c.ChatId == request.ChatId);

            if (userById == null)
                throw new CustomException(CustomExceptionType.NotFound, $"Chat with id:{request.ChatId} wasn't found.");

            var message = CreateMessageDTO.CreateMessageDTOToMessage(request);

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return MessageDTO.MessageToMessageDTO(message);
        }

        public async Task<ChatMessageDTO> UpdateMessageAsync(int id, UpdateMessageDTO request)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
                throw new CustomException(CustomExceptionType.NotFound, $"Message id:{id} wasn't found.");

            UpdateMessageDTO.UpdateMessage(message, request);
            await _context.SaveChangesAsync();

            return ChatMessageDTO.MessageToMessageDTO(message);
        }

        public async Task DeleteMessageAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
                throw new CustomException(CustomExceptionType.NotFound, $"Message id:{id} wasn't found.");

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }
}
