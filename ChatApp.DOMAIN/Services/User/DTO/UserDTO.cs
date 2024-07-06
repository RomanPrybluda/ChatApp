using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class UserDTO
    {

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public static UserDTO UserToUserDTO(User user)
        {
            return new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName
            };
        }
    }
}