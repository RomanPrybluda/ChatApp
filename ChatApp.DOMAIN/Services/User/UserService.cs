using ChatApp.DAL;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DOMAIN
{
    public class UserService
    {
        private readonly ChatAppDbContext _context;

        public UserService(ChatAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null)
                throw new CustomException(CustomExceptionType.NotFound, "Users weren't found.");

            var userDTOs = users.Select(UserDTO.UserToUserDTO).ToList();

            return userDTOs;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new CustomException(CustomExceptionType.NotFound, $"User id {id} wasn't found.");

            var userDTO = UserDTO.UserToUserDTO(user);

            return userDTO;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO request)
        {
            var user = CreateUserDTO.CreateUserDTOToUser(request);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var createdUser = await _context.Users.FindAsync(user.UserId);
            var userDTO = UserDTO.UserToUserDTO(createdUser);

            return userDTO;
        }

        public async Task<UserDTO> UpdateUserAsync(int id, UpdateUserDTO request)
        {
            var userById = await _context.Users.FindAsync(id);

            if (userById == null)
                throw new CustomException(CustomExceptionType.NotFound, $"User id {id} wasn't found.");

            UpdateUserDTO.UpdateUser(userById, request);

            await _context.SaveChangesAsync();

            var updatedUser = await _context.Users.FindAsync(id);
            var userDTO = UserDTO.UserToUserDTO(updatedUser);

            return userDTO;
        }

        public async Task DeleteUserAsync(int id)
        {
            var userById = await _context.Users.FindAsync(id);

            if (userById == null)
                throw new CustomException(CustomExceptionType.NotFound, $"User id {id} wasn't found.");

            _context.Users.Remove(userById);
            await _context.SaveChangesAsync();
        }
    }
}
