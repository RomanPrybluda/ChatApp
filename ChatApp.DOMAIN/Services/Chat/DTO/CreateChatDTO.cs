using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class CreateChatDTO
    {

        public string ChatName { get; set; } = string.Empty;

        public static Chat CreateChatDTOToChat(CreateChatDTO createChatDTO, int userId)
        {
            var chat = new Chat()
            {
                ChatName = createChatDTO.ChatName,
                CreatorUserId = userId
            };

            return chat;
        }

    }
}