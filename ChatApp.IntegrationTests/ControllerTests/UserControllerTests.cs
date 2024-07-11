using ChatApp.DOMAIN;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ChatApp.IntegrationTests
{
    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/user");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserDTO>>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            users.Should().NotBeNull();
            users.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnOk()
        {
            // Arrange
            var userId = 1;

            // Act
            var response = await _client.GetAsync($"/user/{userId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserDTO>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            user.Should().NotBeNull();
            user.UserId.Should().Be(userId);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldReturnOk()
        {
            // Arrange
            var newUser = new CreateUserDTO { UserName = "NewTestUser" };
            var json = JsonSerializer.Serialize(newUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/user", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserDTO>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            user.Should().NotBeNull();
            user.UserName.Should().Be("NewTestUser");
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnOk()
        {
            // Arrange
            var userId = 1;
            var updateUser = new UpdateUserDTO { UserName = "UpdatedTestUser" };
            var json = JsonSerializer.Serialize(updateUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/user/{userId}", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserDTO>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            user.Should().NotBeNull();
            user.UserName.Should().Be("UpdatedTestUser");
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNoContent()
        {
            // Arrange
            var userId = 1;

            // Act
            var response = await _client.DeleteAsync($"/user/{userId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
