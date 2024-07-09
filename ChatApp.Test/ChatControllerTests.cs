using ChatApp.DOMAIN;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;
using System.Text.Json;

namespace ChatApp.IntegrationTests
{
    public class ChatControllerTests
    {
        private readonly HttpClient _client;

        WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });

        public ChatControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllChats_ReturnsSuccessResult()
        {
            // Act
            var response = await _client.GetAsync("/chat");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var chats = JsonSerializer.Deserialize<List<ChatDTO>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.NotNull(chats);
            Assert.NotEmpty(chats);
        }

        [Fact]
        public async Task CreateChat_ReturnsSuccessResult()
        {
            // Arrange
            var newChat = new CreateChatDTO { ChatName = "NewTestChat" };
            var json = JsonSerializer.Serialize(newChat);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/chat?userId=1", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var chat = JsonSerializer.Deserialize<ChatDTO>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.NotNull(chat);
            Assert.Equal("NewTestChat", chat.ChatName);
        }
    }
}
