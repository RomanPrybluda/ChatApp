using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class UpdateUserDTO
    {

        public string UserName { get; set; } = string.Empty;

        public static void UpdateUser(User user, UpdateUserDTO dto)
        {
            user.UserName = dto.UserName;
        }
    }
}