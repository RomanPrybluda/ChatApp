using ChatApp.DAL;
using ChatApp.DOMAIN;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.UnitTests
{
    public class MessageServiceTests
    {
        private readonly MessageService _messageService;
        private readonly ChatAppDbContext _context;
        private MessageDTO _addedMessage;

        public MessageServiceTests()
        {
            var options = new DbContextOptionsBuilder<ChatAppDbContext>()
                .UseInMemoryDatabase(databaseName: "ChatAppTestDb")
                .Options;

            _context = new ChatAppDbContext(options);
            _messageService = new MessageService(_context);

            SeedData();
        }

        private void SeedData()
        {
            if (!_context.Users.Any())
            {
                _context.Users.Add(new User { UserName = "TestUser1" });
                _context.Users.Add(new User { UserName = "TestUser2" });
                _context.SaveChanges();
            }

            if (!_context.Chats.Any())
            {
                _context.Chats.Add(new Chat { ChatName = "TestChat1", CreatorUserId = _context.Users.First().UserId });
                _context.SaveChanges();
            }
        }

        private int GetExistingUserId()
        {
            return _context.Users.First().UserId;
        }

        private int GetExistingChatId()
        {
            return _context.Chats.First().ChatId;
        }

        private string GenerateUniqueMessageText()
        {
            return $"Message_{Guid.NewGuid()}";
        }

        [Fact]
        public async Task Can_Add_Message()
        {
            var createMessageDto = new CreateMessageDTO
            {
                Text = GenerateUniqueMessageText(),
                UserId = GetExistingUserId(),
                ChatId = GetExistingChatId()
            };

            _addedMessage = await _messageService.CreateMessageAsync(createMessageDto);

            Assert.NotNull(_addedMessage);
            Assert.Equal(createMessageDto.Text, _addedMessage.Text);
            Assert.Equal(createMessageDto.UserId, _addedMessage.UserId);
            Assert.Equal(createMessageDto.ChatId, _addedMessage.ChatId);
        }

        [Fact]
        public async Task Can_Delete_Message()
        {
            // Ensure a message is added before attempting to delete
            if (_addedMessage == null)
            {
                await Can_Add_Message();
            }

            await _messageService.DeleteMessageAsync(_addedMessage.MessageId);

            var messages = await _messageService.GetAllChatMessagesAsync(_addedMessage.ChatId);
            Assert.DoesNotContain(messages, m => m.MessageId == _addedMessage.MessageId);
        }
    }
}
