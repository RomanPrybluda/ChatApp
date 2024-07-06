using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class CreateUserDTO
    {

        public string UserName { get; set; } = string.Empty;

        public static User CreateUserDTOToUser(CreateUserDTO dto)
        {
            return new User
            {
                UserName = dto.UserName
            };
        }
    }
}