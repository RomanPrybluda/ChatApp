using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class UserByIdDTO
    {

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public static UserByIdDTO UserToUserByIdDTO(User user)
        {
            return new UserByIdDTO
            {
                UserId = user.UserId,
                UserName = user.UserName
            };
        }
    }
}