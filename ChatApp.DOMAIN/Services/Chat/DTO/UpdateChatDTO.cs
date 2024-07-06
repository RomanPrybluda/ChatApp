using ChatApp.DAL;

namespace ChatApp.DOMAIN
{
    public class UpdateChatDTO
    {

        public string ChatName { get; set; } = string.Empty;

        public static void UpdateChat(Chat chat, UpdateChatDTO updateChatDTO)
        {

            chat.ChatName = updateChatDTO.ChatName;

        }

    }
}