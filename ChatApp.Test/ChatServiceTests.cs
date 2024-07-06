using ChatApp.DAL;
using ChatApp.DOMAIN;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Test
{
    public class ChatServiceTests
    {
        private readonly ChatService _chatService;
        private readonly ChatAppDbContext _context;

        public ChatServiceTests(ChatService chatService)
        {
            var options = new DbContextOptionsBuilder<ChatAppDbContext>()
                .UseInMemoryDatabase(databaseName: "ChatAppTestDb")
                .Options;

            _context = new ChatAppDbContext(options);
            _chatService = chatService;
        }

        //[Fact]
        //public async Task Can_Add_Chat()
        //{
        //    var chat = new Chat { ChatName = "Test Chat" };
        //    await _chatService.CreateChatAsync(chat);
        //    Assert.Equal(1, _context.Chats.Count());
        //}

        //[Fact]
        //public async Task Can_Get_All_Chats()
        //{
        //    var chat = new Chat { ChatName = "Test Chat" };
        //    await _chatService.CreateChatAsync(chat);

        //    var chats = await _chatService.GetAllChatsAsync();
        //    Assert.Single(chats);
        //}

        //[Fact]
        //public async Task Can_Delete_Chat()
        //{
        //    var chat = new Chat { ChatName = "Test Chat" };
        //    await _chatService.CreateChatAsync(chat);
        //    await _chatService.DeleteChatAsync(chat.ChatId);

        //    Assert.Equal(0, _context.Chats.Count());
        //}
    }
}