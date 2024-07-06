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
                throw new CustomException(CustomExceptionType.NotFound,
                    "Chats weren't found.");

            var chatDTOs = new List<ChatDTO>();

            foreach (var chat in chats)
            {
                var chatDTO = ChatDTO.ChatToChatDTO(chat);
                chatDTOs.Add(chatDTO);
            }

            return chatDTOs;
        }

        public async Task<ChatDTO> GetChatByIdAsync(int id)
        {
            var chat = await _context.Chats.FindAsync(id);

            if (chat == null)
                throw new CustomException(CustomExceptionType.NotFound,
                    $"Chat id {id} wasn't found.");

            var chatDTO = ChatDTO.ChatToChatDTO(chat);

            return chatDTO;
        }

        public async Task<ChatDTO> CreateChatAsync(CreateChatDTO request)
        {
            var chatByFullName = await _context.Chats
                .FirstOrDefaultAsync(c => c.ChatName == request.ChatName);

            if (chatByFullName != null)
                throw new CustomException(CustomExceptionType.ChatIsAlreadyExists,
                    $"Chat is already exists.");

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
                throw new CustomException(CustomExceptionType.NotFound,
                    $"Chat id {id} wasn't found.");

            UpdateChatDTO.UpdateChat(chatById, request);

            await _context.SaveChangesAsync();

            var updatedChat = await _context.Chats.FindAsync(id);
            var chatDTO = ChatDTO.ChatToChatDTO(updatedChat);

            return chatDTO;
        }

        public async Task DeleteChatAsync(int id)
        {
            var chatById = await _context.Chats.FindAsync(id);

            if (chatById == null)
                throw new CustomException(CustomExceptionType.NotFound,
                    $"Chat id {id} wasn't found.");

            _context.Chats.Remove(chatById);
            await _context.SaveChangesAsync();
        }
    }
}
