using ChatApp.DAL;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DOMAIN
{
    public class ChatService
    {
        private readonly ChatAppDbContext _context;

        public ChatService(ChatAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChatDTO>> GetAllChatsAsync()
        {
            var chats = await _context.Chats.ToListAsync();

            if (chats == null)
                throw new CustomException(CustomExceptionType.NotFound, "Chats weren't found.");

            var chatDTOs = chats.Select(ChatDTO.ChatToChatDTO).ToList();

            return chatDTOs;
        }

        public async Task<ChatByIdDTO> GetChatByIdAsync(int id)
        {
            var chat = await _context.Chats.Include(c => c.Messages).ThenInclude(m => m.User)
                .FirstOrDefaultAsync(c => c.ChatId == id);

            if (chat == null)
                throw new CustomException(CustomExceptionType.NotFound, $"Chat id {id} wasn't found.");

            var chatDTO = ChatByIdDTO.ChatToChatByIdDTO(chat);

            return chatDTO;
        }

        public async Task<ChatDTO> CreateChatAsync(CreateChatDTO request, int userId)
        {
            var chatByFullName = await _context.Chats.FirstOrDefaultAsync(c => c.ChatName == request.ChatName);

            if (chatByFullName != null)
                throw new CustomException(CustomExceptionType.ChatIsAlreadyExists, "Chat already exists.");

            var chat = CreateChatDTO.CreateChatDTOToChat(request);

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            var createdChat = await _context.Chats.FindAsync(chat.ChatId);
            var chatDTO = ChatDTO.ChatToChatDTO(createdChat);

            return chatDTO;
        }

        public async Task<ChatDTO> UpdateChatAsync(int id, UpdateChatDTO request)
        {
            var chatById = await _context.Chats.FindAsync(id);

            if (chatById == null)
                throw new CustomException(CustomExceptionType.NotFound, $"Chat id {id} wasn't found.");

            UpdateChatDTO.UpdateChat(chatById, request);

            await _context.SaveChangesAsync();

            var updatedChat = await _context.Chats.FindAsync(id);
            var chatDTO = ChatDTO.ChatToChatDTO(updatedChat);

            return chatDTO;
        }

        public async Task DeleteChatAsync(int id, int userId)
        {
            var chatById = await _context.Chats.FindAsync(id);

            if (chatById == null)
                throw new CustomException(CustomExceptionType.NotFound, $"Chat id {id} wasn't found.");

            if (chatById.CreatorUserId != userId)
                throw new CustomException(CustomExceptionType.NoPermissions, "No permissions to perform the operation.");

            _context.Chats.Remove(chatById);
            await _context.SaveChangesAsync();
        }
    }
}