using ChatApp.DAL;
using ChatApp.DOMAIN;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.UnitTests
{
    public class ChatServiceTests
    {
        private readonly ChatService _chatService;
        private readonly ChatAppDbContext _context;
        private ChatDTO _addedChat;

        public ChatServiceTests()
        {
            var options = new DbContextOptionsBuilder<ChatAppDbContext>()
                .UseInMemoryDatabase(databaseName: "ChatAppTestDb")
                .Options;

            _context = new ChatAppDbContext(options);
            _chatService = new ChatService(_context);

            SeedUsers();
        }

        private void SeedUsers()
        {
            if (!_context.Users.Any())
            {
                _context.Users.Add(new User { UserName = "TestUser1" });
                _context.Users.Add(new User { UserName = "TestUser2" });
                _context.Users.Add(new User { UserName = "TestUser3" });
                _context.SaveChanges();
            }
        }

        private int GetExistingUserId()
        {
            var user = _context.Users.FirstOrDefault();
            if (user != null)
            {
                return user.UserId;
            }
            else
            {
                throw new InvalidOperationException("No users available in the database.");
            }
        }

        private string GenerateUniqueChatName()
        {
            return $"Chat_{Guid.NewGuid()}";
        }

        [Fact]
        public async Task Can_Add_Chat()
        {
            var createChatDto = new CreateChatDTO { ChatName = GenerateUniqueChatName() };
            var userId = GetExistingUserId();
            _addedChat = await _chatService.CreateChatAsync(createChatDto, userId);

            Assert.NotNull(_addedChat);
            Assert.Equal(createChatDto.ChatName, _addedChat.ChatName);
            Assert.Equal(userId, _addedChat.CreatorId);
        }

        [Fact]
        public async Task Can_Delete_Chat()
        {
            // Ensure a chat is added before attempting to delete
            if (_addedChat == null)
            {
                await Can_Add_Chat();
            }

            await _chatService.DeleteChatAsync(_addedChat.ChatId, _addedChat.CreatorId);

            var chat = await _context.Chats.FindAsync(_addedChat.ChatId);
            Assert.Null(chat);
        }
    }
}
