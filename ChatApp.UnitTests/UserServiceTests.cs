using ChatApp.DAL;
using ChatApp.DOMAIN;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.UnitTests
{
    public class UserServiceTests : IDisposable
    {
        private readonly UserService _userService;
        private readonly ChatAppDbContext _context;

        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<ChatAppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ChatAppDbContext(options);
            _userService = new UserService(_context);

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

        [Fact]
        public async Task Can_Add_User()
        {
            var createUserDto = new CreateUserDTO { UserName = "NewTestUser" };
            var userDto = await _userService.CreateUserAsync(createUserDto);

            Assert.Equal(4, _context.Users.Count());
            Assert.Equal("NewTestUser", userDto.UserName);
        }

        [Fact]
        public async Task Can_Get_All_Users()
        {
            var users = await _userService.GetAllUsersAsync();
            Assert.Equal(3, users.Count());
        }

        [Fact]
        public async Task Can_Get_User_By_Id()
        {
            var existingUser = _context.Users.First();
            var userDto = await _userService.GetUserByIdAsync(existingUser.UserId);

            Assert.NotNull(userDto);
            Assert.Equal(existingUser.UserName, userDto.UserName);
        }

        [Fact]
        public async Task Can_Update_User()
        {
            var existingUser = _context.Users.First();
            var updateUserDto = new UpdateUserDTO { UserName = "UpdatedTestUser" };

            var updatedUserDto = await _userService.UpdateUserAsync(existingUser.UserId, updateUserDto);

            Assert.Equal("UpdatedTestUser", updatedUserDto.UserName);
        }

        [Fact]
        public async Task Can_Delete_User()
        {
            var existingUser = _context.Users.First();
            await _userService.DeleteUserAsync(existingUser.UserId);

            Assert.Equal(2, _context.Users.Count());
        }

        [Fact]
        public async Task Creating_Duplicate_User_Throws_Exception()
        {
            var createUserDto = new CreateUserDTO { UserName = "TestUser1" };

            await Assert.ThrowsAsync<CustomException>(async () =>
            {
                await _userService.CreateUserAsync(createUserDto);
            });
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
